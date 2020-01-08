using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Repository.Providers.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace BenDing.Service.Providers
{

    public class OutpatientDepartmentService : IOutpatientDepartmentService
    {
        private readonly IOutpatientDepartmentRepository _outpatientDepartmentRepository;
        private readonly IWebServiceBasicService _serviceBasicService;
        private readonly IWebBasicRepository _webBasicRepository;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsuranceRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;


        public OutpatientDepartmentService(
           IOutpatientDepartmentRepository outpatientDepartmentRepository,
           IWebServiceBasicService webServiceBasicService,
           IWebBasicRepository webBasicRepository,
           IHisSqlRepository hisSqlRepository,
           ISystemManageRepository systemManageRepository,
           IResidentMedicalInsuranceRepository residentMedicalInsuranceRepository,
           IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository
           )
        {
            _serviceBasicService = webServiceBasicService;
            _outpatientDepartmentRepository = outpatientDepartmentRepository;
            _webBasicRepository = webBasicRepository;
            _hisSqlRepository = hisSqlRepository;
            _systemManageRepository = systemManageRepository;
            _residentMedicalInsuranceRepository = residentMedicalInsuranceRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
        }

        /// <summary>
        /// 门诊费用录入
        /// </summary>
        public OutpatientDepartmentCostInputDto OutpatientDepartmentCostInput(GetOutpatientPersonParam param)
        {
            OutpatientDepartmentCostInputDto resultData = null;
            //获取门诊病人数据
            var outpatientPerson = _serviceBasicService.GetOutpatientPerson(param);
            if (outpatientPerson == null) throw new Exception("his中未获取到当前病人!!!");
            if (string.IsNullOrWhiteSpace(outpatientPerson.IdCardNo)) throw new Exception("当前病人的身份证号码不能为空!!!");
            var inputParam = new OutpatientDepartmentCostInputParam()
            {
                AllAmount = outpatientPerson.MedicalTreatmentTotalCost,
                IdentityMark = "1",
                InformationNumber = outpatientPerson.IdCardNo,
                Operators = param.User.UserName
            };
            //医保数据写入
            resultData = _outpatientDepartmentRepository.OutpatientDepartmentCostInput(inputParam);
            if (resultData == null) throw new Exception("门诊费用医保执行失败!!!");
          
            param.IsSave = true;
            param.Id = Guid.NewGuid();
            //保存门诊病人
            _serviceBasicService.GetOutpatientPerson(param);
            //中间层数据写入
            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = JsonConvert.SerializeObject(param),
                BusinessId = param.UiParam.BusinessId,
                Id = Guid.NewGuid(),
                IsModify = false,
                InsuranceType = 999,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceHospitalized,
                MedicalInsuranceHospitalizationNo = resultData.DocumentNo
            };
            //存中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(param.User, saveData);

            //日志写入
            _systemManageRepository.AddHospitalLog(new AddHospitalLogParam()
            {
                User = param.User,
                JoinOrOldJson = JsonConvert.SerializeObject(inputParam),
                ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
                RelationId = param.Id,
                Remark = "[R][OutpatientDepartment]门诊病人结算"
            });
            //获取病人的基础信息
            var userInfoData = _residentMedicalInsuranceRepository.GetUserInfo(new ResidentUserInfoParam()
            {
                IdentityMark = "1",
                InformationNumber = outpatientPerson.IdCardNo,
            });
            //回参构建
            var xmlData = new OutpatientDepartmentCostXml()
            {
                AccountBalance= userInfoData.InsuranceType == "342" ? userInfoData.ResidentInsuranceBalance: userInfoData.WorkersInsuranceBalance,
                MedicalInsuranceOutpatientNo = resultData.DocumentNo,
                CashPayment = resultData.SelfPayFeeAmount,
                SettlementNo = resultData.DocumentNo,
                AllAmount = outpatientPerson.MedicalTreatmentTotalCost,
                PatientName = outpatientPerson.PatientName,
                AccountAmountPay = resultData.ReimbursementExpensesAmount,
                MedicalInsuranceType = userInfoData.InsuranceType == "342" ? "10" : userInfoData.InsuranceType,
            };

            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = param.User,
                MedicalInsuranceBackNum = "zydj",
                MedicalInsuranceCode = "48",
                BusinessId = param.UiParam.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _webBasicRepository.SaveXmlData(saveXml);
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.User.UserId,
                ReimbursementExpensesAmount =resultData.ReimbursementExpensesAmount,
                SelfPayFeeAmount = resultData.SelfPayFeeAmount,
                OtherInfo = JsonConvert.SerializeObject(resultData),
                Id = saveData.Id,
                SettlementNo = resultData.DocumentNo,
                MedicalInsuranceAllAmount = outpatientPerson.MedicalTreatmentTotalCost,
                SettlementTransactionId = param.User.TransKey,
                MedicalInsuranceState = MedicalInsuranceState.HisSettlement
            };
            //更新中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);

            return resultData;
        }

        public void CancelOutpatientDepartmentCost(UiBaseDataParam param)
        {
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            //获取医保病人信息
            var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
            {
                BusinessId = param.BusinessId,
                OrganizationCode = userBase.OrganizationCode
            };
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (residentData == null) throw new Exception("当前病人未结算,不能取消结算!!!");
            _outpatientDepartmentRepository.CancelOutpatientDepartmentCost(new CancelOutpatientDepartmentCostParam()
            {
               DocumentNo = residentData.SettlementNo
            });

            //添加日志
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                User = userBase,
                Remark = "门诊取消结算",
                RelationId = residentData.Id,
            };
            _systemManageRepository.AddHospitalLog(logParam);

            //回参构建
            var xmlData = new OutpatientDepartmentCostCancelXml()
            {
              SettlementNo = residentData.SettlementNo
            };
            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = userBase,
                MedicalInsuranceBackNum = "Qxjs",
                MedicalInsuranceCode = "42MZ",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _webBasicRepository.SaveXmlData(saveXml);

            var updateParamData = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.UserId,
                Id = residentData.Id,
                CancelTransactionId = param.TransKey,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceCancelSettlement,
                IsHisUpdateState = true
            };
            //更新中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParamData);

        }
    }
}
