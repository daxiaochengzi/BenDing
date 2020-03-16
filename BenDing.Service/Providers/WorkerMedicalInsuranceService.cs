using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Dto.Workers;
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
using BenDing.Repository.Providers.Web;
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace BenDing.Service.Providers
{
    public class WorkerMedicalInsuranceService : IWorkerMedicalInsuranceService
    {
        private readonly IWorkerMedicalInsuranceRepository _workerMedicalInsuranceRepository;
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsuranceRepository;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IWebBasicRepository _webBasicRepository;

        private readonly IWebServiceBasicService _serviceBasicService;
        public WorkerMedicalInsuranceService(
            IWorkerMedicalInsuranceRepository workerMedicalInsuranceRepository,
            IHisSqlRepository hisSqlRepository,
            IWebBasicRepository webBasicRepository,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            ISystemManageRepository systemManageRepository,
            IResidentMedicalInsuranceRepository residentMedicalInsuranceRepository,
            IWebServiceBasicService webServiceBasic
            )
        {
            _workerMedicalInsuranceRepository = workerMedicalInsuranceRepository;
            _hisSqlRepository = hisSqlRepository;
            _webBasicRepository = webBasicRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _systemManageRepository = systemManageRepository;
            _residentMedicalInsuranceRepository = residentMedicalInsuranceRepository;
            _serviceBasicService = webServiceBasic;
        }
        /// <summary>
        /// 职工入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationRegisterDto WorkerHospitalizationRegister(WorKerHospitalizationRegisterUiParam param)
        {//his登陆
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var infoData = new GetInpatientInfoParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            };
            //获取医保病人
            var inpatientData = _serviceBasicService.GetInpatientInfo(infoData);
            if (inpatientData == null) throw new Exception("获取医保病人失败!!!");
            var workerParam = GetWorkerHospitalizationRegisterParam(param, inpatientData, userBase);
            //医保执行
            var registerData = _workerMedicalInsuranceRepository.WorkerHospitalizationRegister(workerParam);
            if (registerData == null) throw new Exception("职工入院登记未反馈数据!!!");
            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = JsonConvert.SerializeObject(workerParam),
                BusinessId = param.BusinessId,
                Id = Guid.NewGuid(),
                IsModify = false,
                InsuranceType = 310,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceHospitalized,
                MedicalInsuranceHospitalizationNo = registerData.MedicalInsuranceHospitalizationNo
            };
            //存中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(userBase, saveData);
            //回参构建
            var xmlData = new HospitalizationRegisterXml()
            {
                MedicalInsuranceType = "310",
                MedicalInsuranceHospitalizationNo = registerData.MedicalInsuranceHospitalizationNo,
                InsuranceNo = null,
            };
            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = userBase,
                MedicalInsuranceBackNum = "zydj",
                MedicalInsuranceCode = "21",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _webBasicRepository.SaveXmlData(saveXml);
            saveData.MedicalInsuranceState = MedicalInsuranceState.HisHospitalized;
            //更新中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(userBase, saveData);
            //保存入院数据
            infoData.IsSave = true;
            _serviceBasicService.GetInpatientInfo(infoData);
            return registerData;
        }
        /// <summary>
        /// 职工入院登记修改
        /// </summary>
        /// <param name="uiParam"></param>
        public void ModifyWorkerHospitalization(HospitalizationModifyUiParam uiParam)
        { //his登陆
            var userBase = _serviceBasicService.GetUserBaseInfo(uiParam.UserId);
            userBase.TransKey = uiParam.TransKey;
            var param = GetWorkerHospitalizationModify(uiParam, userBase);
            //医保执行
            _workerMedicalInsuranceRepository.ModifyWorkerHospitalization(param);
            // 回参构建
            var xmlData = new HospitalizationModifyXml()
            {
                MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
            };
            //var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            //var saveXml = new SaveXmlDataParam()
            //{
            //    User = param.User,
            //    MedicalInsuranceBackNum = "zyzlxgall",
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
                    OrganizationCode = param.User.OrganizationCode
                });
            if (!string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson))
            {
                var data =
                    JsonConvert.DeserializeObject<WorKerHospitalizationRegisterParam>(queryData
                        .AdmissionInfoJson);
                if (!string.IsNullOrWhiteSpace(param.AdmissionDate))
                    data.AdmissionDate = param.AdmissionDate;
                if (!string.IsNullOrWhiteSpace(param.AdmissionMainDiagnosis))
                    data.AdmissionMainDiagnosis = param.AdmissionMainDiagnosis;
                if (!string.IsNullOrWhiteSpace(param.AdmissionMainDiagnosisIcd10))
                    data.AdmissionMainDiagnosisIcd10 = param.AdmissionMainDiagnosisIcd10;
                if (!string.IsNullOrWhiteSpace(param.DiagnosisIcd10Three))
                    data.DiagnosisIcd10Three = param.DiagnosisIcd10Three;
                if (!string.IsNullOrWhiteSpace(param.DiagnosisIcd10Two))
                    data.DiagnosisIcd10Two = param.DiagnosisIcd10Two;
                if (!string.IsNullOrWhiteSpace(param.InpatientDepartmentCode))
                    data.InpatientDepartmentCode = param.InpatientDepartmentCode;
                if (!string.IsNullOrWhiteSpace(param.BedNumber))
                    data.BedNumber = param.BedNumber;
                data.Id = queryData.Id;
                paramStr = JsonConvert.SerializeObject(data);
            }
            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = paramStr,
                BusinessId = queryData.BusinessId,
                Id = queryData.Id,
                IsModify = true,

            };
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(param.User, saveData);
            //日志
            var logParam = new AddHospitalLogParam();
            logParam.User = param.User;
            logParam.RelationId = queryData.Id;
            logParam.JoinOrOldJson = queryData.AdmissionInfoJson;
            logParam.ReturnOrNewJson = paramStr;
            logParam.Remark = "医保入院登记修改";
            _systemManageRepository.AddHospitalLog(logParam);

        }
        /// <summary>
        /// 职工住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerHospitalizationPreSettlement(UiBaseDataParam param)
        {
            //获取操作人员信息
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
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
            var hisPreSettlementData = _serviceBasicService.GetHisHospitalizationPreSettlement(infoData);
            var preSettlementData = hisPreSettlementData.PreSettlementData.FirstOrDefault();
            //获取医保病人信息
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            //获取医院等级
            var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(userBase.OrganizationCode);
            if (string.IsNullOrWhiteSpace(preSettlementData.EndDate)) throw new Exception("当前病人在基层中未办理出院,不能办理医保预结算!!!");
            //获取医保账号
            var userData = _systemManageRepository.QueryHospitalOperator(
                     new QueryHospitalOperatorParam() { UserId = param.UserId });
            var preSettlement = new WorkerHospitalizationPreSettlementParam()
            {
                User = userBase,
                Id = residentData.Id,
                BusinessId = param.BusinessId,
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                LeaveHospitalDate = Convert.ToDateTime(preSettlementData.EndDate).ToString("yyyyMMdd"),
                AdministrativeArea = gradeData.AdministrativeArea,
                Operators = preSettlementData.Operator,
                IsHospitalizationFrequency = "0",
                OrganizationCode = gradeData.MedicalInsuranceAccount,
            };
            //医保结算
            var resultData = _workerMedicalInsuranceRepository.WorkerHospitalizationPreSettlement(preSettlement);

            //报销金额 =统筹支付+补充医疗保险支付金额+专项基金支付金额+
            //公务员补贴+公务员补助+其它支付金额
            decimal reimbursementExpenses = resultData.BasicOverallPay + resultData.SupplementPayAmount + resultData.SpecialFundPayAmount
            + resultData.CivilServantsSubsidies + resultData.CivilServantsSubsidy + resultData.OtherPaymentAmount;
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {

                ReimbursementExpensesAmount = CommonHelp.ValueToDouble(reimbursementExpenses),
                SelfPayFeeAmount = resultData.CashPayment,
                OtherInfo = JsonConvert.SerializeObject(resultData),
                Id = residentData.Id,
                UserId = userBase.UserId,
                SettlementNo = resultData.DocumentNo,
                MedicalInsuranceAllAmount = resultData.TotalAmount,
                PreSettlementTransactionId = userBase.TransKey,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsurancePreSettlement
            };
            //存入中间库
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
                User = userBase,
                Remark = "职工住院病人预结算"
            };
            _systemManageRepository.AddHospitalLog(logParam);

            return resultData;
        }
        /// <summary>
        /// 职工住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerHospitalizationSettlement(WorkerHospitalizationSettlementUiParam param)
        {
            //获取操作人员信息
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var infoData = new GetInpatientInfoParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            };
            //获取his结算
            var hisSettlement = _serviceBasicService.GetHisHospitalizationSettlement(infoData);
            var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
            {
                BusinessId = param.BusinessId,
                OrganizationCode = userBase.OrganizationCode
            };
            //获取医保病人信息
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (residentData.MedicalInsuranceState == MedicalInsuranceState.HisSettlement) throw new Exception("当前病人已办理医保结算,不能办理预结算!!!");
            if (residentData.MedicalInsuranceState != MedicalInsuranceState.MedicalInsurancePreSettlement) throw new Exception("当前病人未办理预结算,不能办理结算!!!");
            var inpatientInfoParam = new QueryInpatientInfoParam() { BusinessId = param.BusinessId };
            //获取住院病人
            var inpatientInfoData = _hisSqlRepository.QueryInpatientInfo(inpatientInfoParam);
            if (inpatientInfoData == null) throw new Exception("该病人未在中心库中,请检查是否办理医保入院!!!");
            //获取医保账号
            var userData = _systemManageRepository.QueryHospitalOperator(
                     new QueryHospitalOperatorParam() { UserId = param.UserId });
            //获取医院等级
            var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(userBase.OrganizationCode);
            var infoParam = new WorkerHospitalizationSettlementParam()
            {
                User = userBase,
                Id = residentData.Id,
                BusinessId = inpatientInfoData.BusinessId,
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                LeaveHospitalDate = Convert.ToDateTime(hisSettlement.LeaveHospitalDate).ToString("yyyyMMdd"),
                LeaveHospitalState = param.LeaveHospitalInpatientState,
                Operators = userBase.UserName,
                OrganizationCode = gradeData.MedicalInsuranceAccount,
                AdministrativeArea = gradeData.AdministrativeArea
            };
            //获取诊断
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            infoParam.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            infoParam.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            infoParam.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;
            infoParam.LeaveHospitalMainDiagnosis = diagnosisData.DiagnosisDescribe;
            infoParam.IsHospitalizationFrequency = "1";
            // 医保
            var resultData = _workerMedicalInsuranceRepository.WorkerHospitalizationSettlement(infoParam);
            //报销金额 =统筹支付+补充医疗保险支付金额+专项基金支付金额+
            //公务员补贴+公务员补助+其它支付金额
            decimal reimbursementExpenses = resultData.BasicOverallPay + resultData.SupplementPayAmount + resultData.SpecialFundPayAmount
            + resultData.CivilServantsSubsidies + resultData.CivilServantsSubsidy + resultData.OtherPaymentAmount;
            resultData.ReimbursementExpenses = reimbursementExpenses;
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = userBase.UserId,
                ReimbursementExpensesAmount = CommonHelp.ValueToDouble(reimbursementExpenses),
                SelfPayFeeAmount = resultData.CashPayment,
                OtherInfo = JsonConvert.SerializeObject(resultData),
                Id = residentData.Id,
                SettlementNo = resultData.DocumentNo,
                MedicalInsuranceAllAmount = resultData.TotalAmount,
                SettlementTransactionId = userBase.UserId,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceSettlement
            };
            
            //存入中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
            //结算后保存信息
            var saveParam = AutoMapper.Mapper.Map<SaveInpatientSettlementParam>(hisSettlement);
            saveParam.Id = (Guid)inpatientInfoData.Id;
            saveParam.User = userBase;
            saveParam.LeaveHospitalDiagnosisJson = JsonConvert.SerializeObject(param.DiagnosisList);
            _hisSqlRepository.SaveInpatientSettlement(saveParam);
            return resultData;
        }
        /// <summary>
        /// 职工划卡
        /// </summary>
        /// <param name="param"></param>
        public void WorkerStrokeCard(WorkerStrokeCardParam param)
        {

            var userInfoData = _residentMedicalInsuranceRepository.GetUserInfo(new ResidentUserInfoParam()
            {
                IdentityMark = param.IdCardNo.Length==18?"1":"2",
                InformationNumber = param.IdCardNo,

            });
            //回参构建
            var xmlData = new HospitalSettlementXml()
            {

                MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
                CashPayment = param.SelfPayFeeAmount,
                SettlementNo = param.DocumentNo,
                PaidAmount = 0,
                AllAmount = param.AllAmount,
                PatientName = userInfoData.PatientName,
                AccountBalance = userInfoData.WorkersInsuranceBalance,
                AccountAmountPay = param.AccountPayAmount,


            };
            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = param.User,
                MedicalInsuranceBackNum = "fyjs_new",
                MedicalInsuranceCode = "41",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _webBasicRepository.SaveXmlData(saveXml);
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.User.UserId,
                ReimbursementExpensesAmount = param.AccountPayAmount,
                SelfPayFeeAmount = param.SelfPayFeeAmount,
                OtherInfo = JsonConvert.SerializeObject(xmlData),
                Id = param.Id,
                WorkersStrokeCardNo = param.DocumentNo,
                WorkersStrokeCardInfo = JsonConvert.SerializeObject(xmlData),
                MedicalInsuranceState = MedicalInsuranceState.HisSettlement
            };

          //  更新中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
            //添加日志
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = JsonConvert.SerializeObject(xmlData),
                User = param.User,
                Remark = "职工病人结算",
                RelationId = param.Id,
            };
            _systemManageRepository.AddHospitalLog(logParam); //回参构建

        }
        /// <summary>
        ///取消结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string WorkerSettlementCancel(WorkerSettlementCancelParam param)
        {  //划卡取消
            _workerMedicalInsuranceRepository.CancelWorkerStrokeCard(new CancelWorkersStrokeCardParam()
            {   WorkersStrokeCardNo = param.WorkersStrokeCardNo,
                Operate = param.User.UserName,
                Remarks = param.CancelSettlementRemarks

            });
            var resultData = _workerMedicalInsuranceRepository.WorkerSettlementCancel(param);
            //取消交易id
            var cancelTransactionId = param.User.TransKey;
            //回参构建
            var xmlData = new HospitalizationRegisterXml()
            {
                MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
            };
            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = param.User,
                MedicalInsuranceBackNum = "Qxjs",
                MedicalInsuranceCode = "22",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _webBasicRepository.SaveXmlData(saveXml);
            var updateParam = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.User.UserId,
                Id = param.Id,
                CancelTransactionId = cancelTransactionId,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsurancePreSettlement,
                CancelSettlementRemarks= param.CancelSettlementRemarks
            };
            //存入中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParam);
            //添加日志
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = "{ yearSign= " + resultData + " }",
                User = param.User,
                Remark = "职工住院结算取消",
                RelationId = param.Id,
            };
            _systemManageRepository.AddHospitalLog(logParam);

            //if (param.CancelLimit == "2") //取消结算,并删除资料<==>删除资料与取消入院
            //{
            //    //回参构建
            //    var xmlData = new HospitalizationRegisterXml()
            //    {
            //        MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
            //    };
            //    var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            //    var saveXml = new SaveXmlDataParam()
            //    {
            //        User = param.User,
            //        MedicalInsuranceBackNum = "Qxjs",
            //        MedicalInsuranceCode = "22",
            //        BusinessId = param.BusinessId,
            //        BackParam = strXmlBackParam
            //    };
            //    //存基层
            //    _webBasicRepository.SaveXmlData(saveXml);

            //    var updateParamData = new UpdateMedicalInsuranceResidentSettlementParam()
            //    {
            //        UserId = param.User.UserId,
            //        Id = param.Id,
            //        CancelTransactionId = cancelTransactionId,
            //        MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceCancelSettlement,
            //        IsHisUpdateState = true
            //    };
            //    //更新中间层
            //    _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParamData);

            //}
            return resultData;
        }
        /// <summary>
        /// 职工结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public QueryWorkerHospitalizationSettlementDto QueryWorkerHospitalizationSettlement(
           QueryWorkerHospitalizationSettlementParam param)
        {
            var resultData = _workerMedicalInsuranceRepository.QueryWorkerHospitalizationSettlement(param);

            return resultData;
        }
        /// <summary>
        /// 获取职工入院登记修改
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private ModifyWorkerHospitalizationParam GetWorkerHospitalizationModify(HospitalizationModifyUiParam param, UserInfoDto user)
        {
            //获取医院等级
            var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(user.OrganizationCode);
            //医保修改
            var modifyParam = new ModifyWorkerHospitalizationParam()
            {
                AdmissionDate = Convert.ToDateTime(param.AdmissionDate).ToString("yyyyMMdd"),
                BedNumber = param.BedNumber,
                BusinessId = param.BusinessId,
                MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
                HospitalizationNo = CommonHelp.GuidToStr(param.BusinessId),
                AdministrativeArea = gradeData.AdministrativeArea,
                Operators = user.UserName,
                OrganizationCode= gradeData.MedicalInsuranceAccount
            };
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            modifyParam.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            modifyParam.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            modifyParam.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;
            modifyParam.AdmissionMainDiagnosis = diagnosisData.DiagnosisDescribe;
            modifyParam.InpatientDepartmentCode = param.InpatientDepartmentCode;
            modifyParam.User = user;
            return modifyParam;
        }
        /// <summary>
        /// 获取职工入院登记入参
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramDto"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private WorKerHospitalizationRegisterParam GetWorkerHospitalizationRegisterParam(
            WorKerHospitalizationRegisterUiParam param, InpatientInfoDto paramDto, UserInfoDto user)
        {
            var iniParam = new WorKerHospitalizationRegisterParam();
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            iniParam.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            iniParam.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            iniParam.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;
            iniParam.AdmissionMainDiagnosis = diagnosisData.DiagnosisDescribe;
            //获取医院等级
            var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(user.OrganizationCode);
            if (gradeData == null) throw new Exception("获取医院等级失败!!!");
            if (string.IsNullOrWhiteSpace(gradeData.AdministrativeArea)) throw new Exception("当前医院未设置行政区域!!!");
            iniParam.IdentityMark = param.IdentityMark;
            iniParam.AfferentSign = param.AfferentSign;
            iniParam.MedicalCategory = param.MedicalCategory;
            iniParam.AdmissionDate = Convert.ToDateTime(paramDto.AdmissionDate).ToString("yyyyMMdd");
            iniParam.InpatientDepartmentCode = paramDto.InDepartmentId;
            iniParam.BedNumber = paramDto.AdmissionBed;
            iniParam.HospitalizationNo = paramDto.HospitalizationNo;
            iniParam.Operators = paramDto.AdmissionOperator;
            iniParam.BusinessId = param.BusinessId;
            iniParam.AdministrativeArea = gradeData.AdministrativeArea;
            iniParam.InpatientArea = paramDto.AdmissionWard;
            //iniParam.DiagnosisList = param.DiagnosisList;
            var userData = _systemManageRepository.QueryHospitalOperator(
                new QueryHospitalOperatorParam() { UserId = param.UserId });
            iniParam.OrganizationCode = gradeData.MedicalInsuranceAccount;
            return iniParam;
        }

    }
}
