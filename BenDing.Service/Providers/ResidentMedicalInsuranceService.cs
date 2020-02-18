using System;
using System.Linq;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.HisXml;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.SystemManage;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;
using BenDing.Domain.Models.Params.Workers;
using BenDing.Domain.Xml;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace BenDing.Service.Providers
{
    public class ResidentMedicalInsuranceService : IResidentMedicalInsuranceService
    {
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsuranceRepository;
        private readonly IWebBasicRepository _webBasicRepository;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IWebServiceBasicService _webserviceBasicService;
      

        public ResidentMedicalInsuranceService(
            IResidentMedicalInsuranceRepository residentMedicalInsuranceRepository,
            IWebBasicRepository webBasicRepository,
            IHisSqlRepository hisSqlRepository,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            ISystemManageRepository systemManageRepository,
            IWebServiceBasicService serviceBasicService
           
            )
        {
            _residentMedicalInsuranceRepository = residentMedicalInsuranceRepository;
            _webBasicRepository = webBasicRepository;
            _hisSqlRepository = hisSqlRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _systemManageRepository = systemManageRepository;
            _webserviceBasicService = serviceBasicService;
           
        }
        public ResidentUserInfoDto GetUserInfo(ResidentUserInfoParam param)
        {
            var resultData = _residentMedicalInsuranceRepository.GetUserInfo(param);

            return resultData;
        }

        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public void HospitalizationRegister(ResidentHospitalizationRegisterUiParam param)
        { //his登陆
            var userBase = _webserviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var infoData = new GetInpatientInfoParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            };
            //获取医保病人
            var inpatientData = _webserviceBasicService.GetInpatientInfo(infoData);
            var registerParam = GetResidentHospitalizationRegisterParam(param, inpatientData);
            var residentData = _residentMedicalInsuranceRepository.HospitalizationRegister(registerParam);
            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = JsonConvert.SerializeObject(param),
                BusinessId = param.BusinessId,
                Id = Guid.NewGuid(),
                InsuranceType = Convert.ToInt32(param.InsuranceType),
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceInpatientNo,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceHospitalized
            };
            //保存中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(userBase, saveData);
            //回参构建
            var xmlData = new HospitalizationRegisterXml()
            {
                MedicalInsuranceType = "10",
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceInpatientNo,
                InsuranceNo = null,
            };
            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = userBase,
                MedicalInsuranceBackNum = "CXJB002",
                MedicalInsuranceCode = "21",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _webBasicRepository.SaveXmlData(saveXml);
            //更新中间库
            saveData.MedicalInsuranceState = MedicalInsuranceState.HisHospitalized;
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(userBase, saveData);
            //日志
            var logParam = new AddHospitalLogParam
            {
                User = userBase,
                RelationId = saveData.Id,
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = JsonConvert.SerializeObject(residentData),
                Remark = "医保入院登记;TransactionId:" + userBase.TransKey
            };
            _systemManageRepository.AddHospitalLog(logParam);
            //保存入院数据
            infoData.IsSave = true;
            _webserviceBasicService.GetInpatientInfo(infoData);

        }
        /// <summary>
        /// 入院登记修改
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user"></param>
        public void HospitalizationModify(HospitalizationModifyUiParam param)
        {//his登陆
            var userBase = _webserviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var modifyParam = GetResidentHospitalizationModify(param);
            _residentMedicalInsuranceRepository.HospitalizationModify(modifyParam, userBase);
            //// 回参构建
            //var xmlData = new HospitalizationRegisterXml()
            //{
            //    MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
            //};
            //var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            //var saveXml = new SaveXmlDataParam()
            //{
            //    User = userBase,
            //    MedicalInsuranceBackNum = "CXJB003",
            //    MedicalInsuranceCode = "23",
            //    BusinessId = param.BusinessId,
            //    BackParam = strXmlBackParam
            //};
            ////存基层
            //_webBasicRepository.SaveXmlData(saveXml);
            var paramStr = "";
            var queryData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(
                new QueryMedicalInsuranceResidentInfoParam
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = userBase.OrganizationCode
                });
            if (!string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson))
            {
                var data =
                    JsonConvert.DeserializeObject<QueryMedicalInsuranceDetailDto>(queryData
                        .AdmissionInfoJson);
                if (!string.IsNullOrWhiteSpace(param.AdmissionDate))
                    data.AdmissionDate = param.AdmissionDate;
                if (!string.IsNullOrWhiteSpace(modifyParam.AdmissionMainDiagnosis))
                    data.AdmissionMainDiagnosis = modifyParam.AdmissionMainDiagnosis;
                if (!string.IsNullOrWhiteSpace(modifyParam.AdmissionMainDiagnosisIcd10))
                    data.AdmissionMainDiagnosisIcd10 = modifyParam.AdmissionMainDiagnosisIcd10;
                if (!string.IsNullOrWhiteSpace(modifyParam.DiagnosisIcd10Three))
                    data.DiagnosisIcd10Three = modifyParam.DiagnosisIcd10Three;
                if (!string.IsNullOrWhiteSpace(modifyParam.DiagnosisIcd10Two))
                    data.DiagnosisIcd10Two = modifyParam.DiagnosisIcd10Two;
                if (!string.IsNullOrWhiteSpace(param.FetusNumber)) data.FetusNumber = param.FetusNumber;
                if (!string.IsNullOrWhiteSpace(param.HouseholdNature))
                    data.HouseholdNature = param.HouseholdNature;
                if (!string.IsNullOrWhiteSpace(param.InpatientDepartmentCode))
                    data.InpatientDepartmentCode = param.InpatientDepartmentCode;
                if (!string.IsNullOrWhiteSpace(param.BedNumber)) data.BedNumber = param.BedNumber;
                data.Id = queryData.Id;
                paramStr = JsonConvert.SerializeObject(data);
            }

            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = paramStr,
                BusinessId = queryData.BusinessId,
                Id = queryData.Id,
                IsModify = true,
                MedicalInsuranceHospitalizationNo = queryData.MedicalInsuranceHospitalizationNo
            };
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(userBase, saveData);
            //日志
            var logParam = new AddHospitalLogParam
            {
                User = userBase,
                RelationId = queryData.Id,
                JoinOrOldJson = queryData.AdmissionInfoJson,
                ReturnOrNewJson = paramStr,
                Remark = "医保入院登记修改"
            };
            _systemManageRepository.AddHospitalLog(logParam);
        }
        /// <summary>
        /// 处方自动上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user"></param>
        public void PrescriptionUploadAutomatic(PrescriptionUploadAutomaticParam param, UserInfoDto user)
        {//获取所有未传费用病人
            var allPatients = _hisSqlRepository.QueryAllHospitalizationPatients(param);
            if (allPatients.Any())
            { //根据组织机构分组
                var organizationGroupBy = allPatients.Select(c => c.OrganizationCode).Distinct().ToList();
                foreach (var item in organizationGroupBy)
                {    //本院获取病人列表
                    var ratientsList = allPatients.Where(c => c.OrganizationCode == item).ToList();
                    //病人传费
                    foreach (var items in ratientsList)
                    {
                        var uploadParam = new PrescriptionUploadUiParam()
                        {
                            BusinessId = items.BusinessId
                        };
                        _residentMedicalInsuranceRepository.PrescriptionUpload(uploadParam, user);
                    }

                }
            }
        }
        /// <summary>
        /// 居民住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public HospitalizationPresettlementDto HospitalizationPreSettlement(UiBaseDataParam param)
        {
            HospitalizationPresettlementDto resultData = null;
            //获取操作人员信息
            var userBase = _webserviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
            {
                BusinessId = param.BusinessId,
                OrganizationCode = userBase.OrganizationCode
            };
            var infoData = new GetInpatientInfoParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            };
            //获取his预结算
            var hisPreSettlementData = _webserviceBasicService.GetHisHospitalizationPreSettlement(infoData);
            var preSettlementData = hisPreSettlementData.PreSettlementData.FirstOrDefault();
            //获取医保病人信息
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (string.IsNullOrWhiteSpace(preSettlementData.EndDate)) throw  new  Exception("当前病人在基层中未办理出院,不能办理医保预结算!!!");
            //医保执行
            var data = _residentMedicalInsuranceRepository.HospitalizationPreSettlement(new HospitalizationPresettlementParam()
            {
                LeaveHospitalDate =Convert.ToDateTime(preSettlementData.EndDate).ToString("yyyyMMdd"),
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
            });
            resultData = data ?? throw new Exception("居民预结算返回结果有误!!!");
            //报销金额 =统筹支付+补充险支付+生育补助+民政救助+民政重大疾病救助+精准扶贫+民政优抚+其它支付
            decimal reimbursementExpenses =
                data.BasicOverallPay + data.SupplementPayAmount + data.BirthAallowance +
                data.CivilAssistancePayAmount + data.CivilAssistanceSeriousIllnessPayAmount +
                data.AccurateAssistancePayAmount + data.CivilServicessistancePayAmount +
                data.OtherPaymentAmount;
            resultData.ReimbursementExpenses = reimbursementExpenses;
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                ReimbursementExpensesAmount = CommonHelp.ValueToDouble(reimbursementExpenses),
                SelfPayFeeAmount = data.CashPayment,
                OtherInfo = JsonConvert.SerializeObject(data),
                Id = residentData.Id,
                UserId = param.UserId,
                SettlementNo = data.DocumentNo,
                MedicalInsuranceAllAmount = data.TotalAmount,
                PreSettlementTransactionId = param.TransKey,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsurancePreSettlement
            };
            //存入中间库
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = JsonConvert.SerializeObject(data),
                User = userBase,

                Remark = "居民住院病人预结算"
            };
            _systemManageRepository.AddHospitalLog(logParam);
            return resultData;
        }
        /// <summary>
        /// 居民住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public HospitalizationPresettlementDto LeaveHospitalSettlement(LeaveHospitalSettlementUiParam param)
        {

            // 获取操作人员信息
            var userBase = _webserviceBasicService.GetUserBaseInfo(param.UserId);
            var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
            {
                BusinessId = param.BusinessId,
                OrganizationCode = userBase.OrganizationCode
            };
            userBase.TransKey = param.TransKey;
            var infoData = new GetInpatientInfoParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            };
            //获取his结算
            var hisSettlement = _webserviceBasicService.GetHisHospitalizationSettlement(infoData);
            //获取医保病人信息
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (residentData.MedicalInsuranceState != MedicalInsuranceState.MedicalInsurancePreSettlement) throw new Exception("当前病人未办理预结算,不能办理结算!!!");
            if (residentData.MedicalInsuranceState == MedicalInsuranceState.HisSettlement) throw new Exception("当前病人已办理医保结算,不能办理再次结算!!!");
           
            var inpatientInfoParam = new QueryInpatientInfoParam() { BusinessId = param.BusinessId };
            //获取住院病人
            var inpatientInfoData = _hisSqlRepository.QueryInpatientInfo(inpatientInfoParam);
            if (inpatientInfoData == null) throw new Exception("该病人未在中心库中,请检查是否办理医保入院!!!");

            var settlementParam = new LeaveHospitalSettlementParam()
            {
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                LeaveHospitalDate = Convert.ToDateTime(hisSettlement.LeaveHospitalDate).ToString("yyyyMMdd"),
                UserId = hisSettlement.LeaveHospitalOperator,
                LeaveHospitalInpatientState = param.LeaveHospitalInpatientState,
            };
            //获取诊断
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            settlementParam.LeaveHospitalMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            settlementParam.LeaveHospitalDiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            settlementParam.LeaveHospitalDiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;
            settlementParam.LeaveHospitalMainDiagnosis = diagnosisData.DiagnosisDescribe;
            var infoParam = new LeaveHospitalSettlementInfoParam()
            {
                User = userBase,
                Id = residentData.Id,
                InsuranceNo = residentData.InsuranceNo,
                BusinessId = inpatientInfoData.BusinessId,
                IdCardNo = inpatientInfoData.IdCardNo,
            };
            //医保执行
            var data = _residentMedicalInsuranceRepository.LeaveHospitalSettlement(settlementParam, infoParam);
            if (data == null) throw new Exception("居民住院结算反馈失败");
            //结算后保存信息
            var saveParam = AutoMapper.Mapper.Map<SaveInpatientSettlementParam>(hisSettlement);
            saveParam.Id = (Guid)inpatientInfoData.Id;
            saveParam.User = userBase;
            saveParam.LeaveHospitalDiagnosisJson = JsonConvert.SerializeObject(param.DiagnosisList);
            _hisSqlRepository.SaveInpatientSettlement(saveParam);
            return data;
        }
        /// <summary>
        /// 医保项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string ProjectDownload(ResidentProjectDownloadParam param)
        {
            ResidentProjectDownloadDto data;
            var pageIni = param.Page;

            param.QueryType = 1;
            var updateTime = _medicalInsuranceSqlRepository.ProjectDownloadTimeMax();
            if (!string.IsNullOrWhiteSpace(updateTime)) param.UpdateTime = Convert.ToDateTime(updateTime).ToString("yyyyMMdd");

            var count = _residentMedicalInsuranceRepository.ProjectDownloadCount(param);
            var cnt = Convert.ToInt32(count / param.Limit) + ((count % param.Limit) > 0 ? 1 : 0);
            param.QueryType = 2;
            Int64 allNum = 0;
            var i = 0;
            while (i < cnt)
            {
                param.Page = i + pageIni;
                data = _residentMedicalInsuranceRepository.ProjectDownload(param);
                if (data != null && data.Row.Any())
                {
                    allNum += data.Row.Count;
                    _medicalInsuranceSqlRepository.ProjectDownload(new UserInfoDto() { UserId = param.UserId }, data.Row);
                }
                i++;
            }
            var resultData = "下载成功共" + allNum + "条记录";
            return resultData;
        }

        /// <summary>
        /// 医保登陆
        /// </summary>
        /// <param name="param"></param>
        public void Login(QueryHospitalOperatorParam param)
        {
            var userBase = _webserviceBasicService.GetUserBaseInfo(param.UserId);
            _residentMedicalInsuranceRepository.Login(userBase.OrganizationCode);
        }

        /// <summary>
        /// 居民入院登记修改
        /// </summary>
        /// <param name="param"></param>
        private HospitalizationModifyParam GetResidentHospitalizationModify(HospitalizationModifyUiParam param)
        {
            //主诊断
            //医保修改
            var modifyParam = new HospitalizationModifyParam()
            {
                AdmissionDate = Convert.ToDateTime(param.AdmissionDate).ToString("yyyyMMdd"),
                BedNumber = param.BedNumber,
                BusinessId = param.BusinessId,
                FetusNumber = param.FetusNumber,
                HouseholdNature = param.HouseholdNature,
                MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
                TransKey = param.TransKey,
                UserId = param.UserId,
                InpatientDepartmentCode = param.InpatientDepartmentCode,
                HospitalizationNo = CommonHelp.GuidToStr(param.BusinessId)
            };
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            modifyParam.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            modifyParam.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            modifyParam.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;

            return modifyParam;
        }
        /// <summary>
        /// 获取居民入院登记入参
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramDto"></param>
        /// <returns></returns>
        private ResidentHospitalizationRegisterParam GetResidentHospitalizationRegisterParam(
            ResidentHospitalizationRegisterUiParam param, InpatientInfoDto paramDto)
        {
            var iniParam = new ResidentHospitalizationRegisterParam();
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            iniParam.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            iniParam.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            iniParam.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;
            iniParam.AdmissionMainDiagnosis = diagnosisData.DiagnosisDescribe;
            iniParam.IdentityMark = param.IdentityMark;
            iniParam.AfferentSign = param.IdentityMark == "1" ? paramDto.IdCardNo : param.AfferentSign;
            iniParam.MedicalCategory = param.MedicalCategory;
            iniParam.FetusNumber = param.FetusNumber;
            iniParam.HouseholdNature = param.HouseholdNature;
            iniParam.AdmissionDate = Convert.ToDateTime(paramDto.AdmissionDate).ToString("yyyyMMdd");
            iniParam.InpatientDepartmentCode = paramDto.InDepartmentId;
            iniParam.InpatientDepartmentName = paramDto.InDepartmentName;
            iniParam.BedNumber = paramDto.AdmissionBed;
            iniParam.HospitalizationNo = paramDto.HospitalizationNo;
            iniParam.Operators = paramDto.AdmissionOperator;
            iniParam.InsuranceType = param.InsuranceType;
            iniParam.BusinessId = param.BusinessId;
            return iniParam;
        }


    }
}
