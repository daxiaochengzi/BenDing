using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Models.Params.Workers;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace BenDing.Service.Providers
{
    public class WorkerMedicalInsuranceService : IWorkerMedicalInsuranceService
    {
        private readonly IWorkerMedicalInsuranceRepository _workerMedicalInsuranceRepository;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IWebBasicRepository _webServiceBasic;
        public WorkerMedicalInsuranceService(
            IWorkerMedicalInsuranceRepository workerMedicalInsuranceRepository,
            IHisSqlRepository hisSqlRepository,
            IWebBasicRepository webBasicRepository,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            ISystemManageRepository systemManageRepository)
        {
            _workerMedicalInsuranceRepository = workerMedicalInsuranceRepository;
            _hisSqlRepository = hisSqlRepository;
            _webServiceBasic = webBasicRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _systemManageRepository = systemManageRepository;
        }
        /// <summary>
        /// 居民职工入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationRegisterDto WorkerHospitalizationRegister(WorKerHospitalizationRegisterParam param)
        {
            var registerData = _workerMedicalInsuranceRepository.WorkerHospitalizationRegister(param);
            if (registerData != null && string.IsNullOrWhiteSpace(registerData.MedicalInsuranceHospitalizationNo))
            {

                var saveData = new MedicalInsuranceDto
                {
                    AdmissionInfoJson = JsonConvert.SerializeObject(param),
                    BusinessId = param.BusinessId,
                    Id = Guid.NewGuid(),
                    IsModify = false,
                    AdministrativeArea= param.AdministrativeArea,
                    InsuranceType = 310,
                    MedicalInsuranceState= MedicalInsuranceState.MedicalInsuranceHospitalized,
                    MedicalInsuranceHospitalizationNo = registerData.MedicalInsuranceHospitalizationNo
                };
                //存中间库
                _medicalInsuranceSqlRepository.SaveMedicalInsurance(param.User, saveData);
                var data = XmlHelp.DeSerializerModel(new ResidentHospitalizationRegisterDto(), true);

                //更新医保信息
                var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
                var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
                var saveXmlData = new SaveXmlData();
                saveXmlData.OrganizationCode = param.User.OrganizationCode;
                saveXmlData.AuthCode = param.User.AuthCode;
                saveXmlData.BusinessId = param.BusinessId;
                saveXmlData.TransactionId = saveData.Id.ToString("N");
                saveXmlData.MedicalInsuranceBackNum = "zydj";
                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.MedicalInsuranceCode = "21";
                saveXmlData.UserId = param.User.UserId;
                //存基层
                _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
                saveData.MedicalInsuranceState = MedicalInsuranceState.HisHospitalized;
               
                //更新中间库
                _medicalInsuranceSqlRepository.SaveMedicalInsurance(param.User, saveData);
            }
            return new WorkerHospitalizationRegisterDto();
        }
        /// <summary>
        /// 职工入院登记修改
        /// </summary>
        /// <param name="param"></param>
        public void ModifyWorkerHospitalization(ModifyWorkerHospitalizationParam param)
        {
            _workerMedicalInsuranceRepository.ModifyWorkerHospitalization(param);

            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = JsonConvert.SerializeObject(param),
                BusinessId = param.BusinessId,
                AdministrativeArea= param.AdministrativeArea,
                Id = param.Id,
                IsModify = true,
               
            };
            //存中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(param.User, saveData);
        }
        /// <summary>
        /// 职工住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerHospitalizationPreSettlement(WorkerHospitalizationPreSettlementParam param)
        {
            var resultData = _workerMedicalInsuranceRepository.WorkerHospitalizationPreSettlement(param);

            //报销金额 =统筹支付+补充医疗保险支付金额+专项基金支付金额+
            //公务员补贴+公务员补助+其它支付金额
            decimal reimbursementExpenses = resultData.BasicOverallPay+ resultData.SupplementPayAmount+ resultData.SpecialFundPayAmount
            +resultData.CivilServantsSubsidies + resultData.CivilServantsSubsidy + resultData.OtherPaymentAmount;
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {

                ReimbursementExpensesAmount = CommonHelp.ValueToDouble(reimbursementExpenses),
                SelfPayFeeAmount = resultData.CashPayment,
                OtherInfo = JsonConvert.SerializeObject(resultData),
                Id = param.Id,
                UserId = param.User.UserId,
                SettlementNo = resultData.DocumentNo,
                MedicalInsuranceAllAmount = resultData.TotalAmount,
                PreSettlementTransactionId = Guid.NewGuid().ToString("N"),
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsurancePreSettlement
            };
            //存入中间库
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
                User = param.User,
                Remark = "职工住院病人预结算"
            };
            _systemManageRepository.AddHospitalLog(logParam);
            //更新医保信息
            var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
            var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
            var saveXmlData = new SaveXmlData();
            saveXmlData.OrganizationCode = param.User.OrganizationCode;
            saveXmlData.AuthCode = param.User.AuthCode;
            saveXmlData.BusinessId = param.BusinessId;
            saveXmlData.TransactionId = updateParam.PreSettlementTransactionId;
            saveXmlData.MedicalInsuranceBackNum = "fyyjs_new";
            saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
            saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
            saveXmlData.MedicalInsuranceCode = "43";
            saveXmlData.UserId = param.User.UserId;
            //存基层
            //_webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
            //更新中间库
            updateParam.IsHisUpdateState = true;
            updateParam.MedicalInsuranceState = MedicalInsuranceState.HisPreSettlement;
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);

            return resultData;
        }
        /// <summary>
        /// 职工住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerHospitalizationSettlement(WorkerHospitalizationSettlementParam param)
        {
            var resultData = _workerMedicalInsuranceRepository.WorkerHospitalizationSettlement(param);
            //报销金额 =统筹支付+补充医疗保险支付金额+专项基金支付金额+
            //公务员补贴+公务员补助+其它支付金额
            decimal reimbursementExpenses = resultData.BasicOverallPay + resultData.SupplementPayAmount + resultData.SpecialFundPayAmount
            + resultData.CivilServantsSubsidies + resultData.CivilServantsSubsidy + resultData.OtherPaymentAmount;
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.User.UserId,
                ReimbursementExpensesAmount = CommonHelp.ValueToDouble(reimbursementExpenses),
                SelfPayFeeAmount = resultData.CashPayment,
                OtherInfo = JsonConvert.SerializeObject(resultData),
                Id = param.Id,
                SettlementNo = resultData.DocumentNo,
                MedicalInsuranceAllAmount = resultData.TotalAmount,
                SettlementTransactionId = Guid.NewGuid().ToString("N"),
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceSettlement
            };
            //存入中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);

            //更新医保信息
            var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
            var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
            var saveXmlData = new SaveXmlData();
            saveXmlData.OrganizationCode = param.User.OrganizationCode;
            saveXmlData.AuthCode = param.User.AuthCode;
            saveXmlData.BusinessId = param.BusinessId;
            saveXmlData.TransactionId = updateParam.SettlementTransactionId;
            saveXmlData.MedicalInsuranceBackNum = "CXJB009";
            saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
            saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
            saveXmlData.MedicalInsuranceCode = "41";
            saveXmlData.UserId = param.User.UserId;
            //存基层
            // _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
            //更新中间层
            updateParam.IsHisUpdateState = true;
            updateParam.MedicalInsuranceState = MedicalInsuranceState.HisSettlement;
            //添加日志
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
                User = param.User,

                Remark = "职工住院病人出院结算",
                RelationId = param.Id,
            };

            _systemManageRepository.AddHospitalLog(logParam);
            return resultData;
        }
        /// <summary>
        ///取消结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string WorkerSettlementCancel(WorkerSettlementCancelParam param)
        {
            var resultData = _workerMedicalInsuranceRepository.WorkerSettlementCancel(param);
            //取消交易id
            var cancelTransactionId = Guid.NewGuid().ToString("N");
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.User.UserId,
                Id = param.Id,
                CancelTransactionId = cancelTransactionId,
                MedicalInsuranceState = MedicalInsuranceState.HisPreSettlement
            };
            //存入中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
            //添加日志
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = "{ yearSign= "+ resultData + " }",
                User = param.User,
                Remark = "职工住院结算取消",
                RelationId = param.Id,
            };

            _systemManageRepository.AddHospitalLog(logParam);
           
            if (param.CancelLimit == "2") //取消结算,并删除资料<==>删除资料与取消入院
            {
                //his取消入院登记
                var strXmlIntoParam = XmlSerializeHelper.XmlSerialize(param);
                var strXmlBackParam = XmlSerializeHelper.XmlBackParam();
                var saveXmlData = new SaveXmlData();
                saveXmlData.OrganizationCode = param.User.OrganizationCode;
                saveXmlData.AuthCode = param.User.AuthCode;
                saveXmlData.BusinessId = param.BusinessId;
                saveXmlData.TransactionId = cancelTransactionId;
                saveXmlData.MedicalInsuranceBackNum = "Qxjs";
                saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
                saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
                saveXmlData.MedicalInsuranceCode = "22";
                saveXmlData.UserId = param.User.UserId;

                _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));

                var updateParamData = new UpdateMedicalInsuranceResidentSettlementParam()
                {
                    UserId = param.User.UserId,
                    Id = param.Id,
                    CancelTransactionId = cancelTransactionId,
                    MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceCancelHospitalized,
                    IsHisUpdateState = true
                };
           //更新中间层
                _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParamData);
               
            }
            return resultData;
        }
    }
}
