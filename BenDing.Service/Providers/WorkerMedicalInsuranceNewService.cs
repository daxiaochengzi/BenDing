using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntity;
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
using BenDing.Service.Interfaces;
using Newtonsoft.Json;

namespace BenDing.Service.Providers
{
   public class WorkerMedicalInsuranceNewService: IWorkerMedicalInsuranceNewService
    {
        private readonly IWorkerMedicalInsuranceRepository _workerMedicalInsuranceRepository;
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsuranceRepository;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IWebBasicRepository _webBasicRepository;

        private readonly IWebServiceBasicService _serviceBasicService;
        public WorkerMedicalInsuranceNewService(
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
        /// 获取职工入院登记参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorKerHospitalizationRegisterParam GetWorkerHospitalizationRegisterParam(WorKerHospitalizationRegisterUiParam param)
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
            var workerParam = GetWorkerHospitalizationRegister(param, inpatientData, userBase);

            return workerParam;
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
            var workerParam = GetWorkerHospitalizationRegister(param, inpatientData, userBase);
            var registerData = JsonConvert.DeserializeObject<WorkerHospitalizationRegisterDto>(param.SettlementJson);
            ////医保执行
            //var registerData = _workerMedicalInsuranceRepository.WorkerHospitalizationRegister(workerParam);
            //if (registerData == null) throw new Exception("职工入院登记未反馈数据!!!");
            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = JsonConvert.SerializeObject(workerParam),
                BusinessId = param.BusinessId,
                Id = Guid.NewGuid(),
                IsModify = false,
                InsuranceType = 310,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceHospitalized,
                MedicalInsuranceHospitalizationNo = registerData.MedicalInsuranceHospitalizationNo,
                AfferentSign = param.AfferentSign,
                IdentityMark = param.IdentityMark
            };
            //存中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(userBase, saveData);
            //回参构建
            var xmlData = new HospitalizationRegisterXml()
            {
                MedicalInsuranceType = "1",
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
        /// 获取职工入院登记修改参数
        /// </summary>
        /// <param name="uiParam"></param>
        /// <returns></returns>
        public ModifyWorkerHospitalizationParam GetModifyWorkerHospitalizationParam(HospitalizationModifyUiParam uiParam)
        {//his登陆
            var userBase = _serviceBasicService.GetUserBaseInfo(uiParam.UserId);
            userBase.TransKey = uiParam.TransKey;
            var resultData = GetWorkerHospitalizationModify(uiParam, userBase);

            return resultData;
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
        /// 职工住院预结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementParam GetWorkerHospitalizationPreSettlementParam(HospitalizationPreSettlementUiParam  param)
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
            return preSettlement;
        }
        /// <summary>
        /// 职工住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerHospitalizationPreSettlement(HospitalizationPreSettlementUiParam  param)
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
            ////医保结算
            //var resultData = _workerMedicalInsuranceRepository.WorkerHospitalizationPreSettlement(preSettlement);

            var resultData = JsonConvert.DeserializeObject<WorkerHospitalizationPreSettlementDto>(param.SettlementJson);
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
        /// 职工住院预结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationSettlementParam GetWorkerHospitalizationSettlementParam(WorkerHospitalizationSettlementUiParam param)
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
            return infoParam;
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
            // 医保执行WorkerHospitalizationPreSettlementDto
            //var resultData = _workerMedicalInsuranceRepository.WorkerHospitalizationSettlement(infoParam);
            //报销金额 =统筹支付+补充医疗保险支付金额+专项基金支付金额+
            //公务员补贴+公务员补助+其它支付金额
            var resultData = JsonConvert.DeserializeObject<WorkerHospitalizationPreSettlementDto>(param.SettlementJson);
            decimal reimbursementExpenses = resultData.BasicOverallPay + resultData.SupplementPayAmount + resultData.SpecialFundPayAmount
            + resultData.CivilServantsSubsidies + resultData.CivilServantsSubsidy + resultData.OtherPaymentAmount;
            resultData.ReimbursementExpenses = reimbursementExpenses;
            var updateData = new UpdateMedicalInsuranceResidentSettlementParam()
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
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateData);
            //添加日志
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
                User = userBase,
                Remark = "职工住院结算",
                RelationId = residentData.Id,
            };
            //存入基层
            var userInfoData = _residentMedicalInsuranceRepository.GetUserInfo(new ResidentUserInfoParam()
            {
                IdentityMark = residentData.IdentityMark,
                AfferentSign = residentData.AfferentSign,

            });

            // 回参构建
            var xmlData = new HospitalSettlementXml()
            {

                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                CashPayment = resultData.CashPayment,
                SettlementNo = resultData.DocumentNo,
                PaidAmount = resultData.PaidAmount,
                AllAmount = resultData.TotalAmount,
                PatientName = userInfoData.PatientName,
                AccountBalance = userInfoData.WorkersInsuranceBalance,
                AccountAmountPay = resultData.AccountPayment,
            };


            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = userBase,
                MedicalInsuranceBackNum = resultData.DocumentNo,
                MedicalInsuranceCode = "41",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //结算存基层
            _webBasicRepository.SaveXmlData(saveXml);

            var updateParamData = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.UserId,
                Id = residentData.Id,
                MedicalInsuranceState = MedicalInsuranceState.HisSettlement,
                IsHisUpdateState = true
            };
            //  更新中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParamData);


            //结算后保存信息
            var saveParam = AutoMapper.Mapper.Map<SaveInpatientSettlementParam>(hisSettlement);
            saveParam.Id = (Guid)inpatientInfoData.Id;
            saveParam.User = userBase;
            saveParam.LeaveHospitalDiagnosisJson = JsonConvert.SerializeObject(param.DiagnosisList);
            _hisSqlRepository.SaveInpatientSettlement(saveParam);
            return resultData;
        }
        /// <summary>
        /// 获取职工入院登记入参
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramDto"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private WorKerHospitalizationRegisterParam GetWorkerHospitalizationRegister(
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
                OrganizationCode = gradeData.MedicalInsuranceAccount
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
        ///取消结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string WorkerSettlementCancel(WorkerSettlementCancelParam param)
        {
            string resultData = null;
            if (param.CancelLimit == "1")
            {
                ////划卡取消
                //if (param.WorkersStrokeCardNo != null)
                //{
                //    _workerMedicalInsuranceRepository.CancelWorkerStrokeCard(new CancelWorkersStrokeCardParam()
                //    {
                //        WorkersStrokeCardNo = param.WorkersStrokeCardNo,
                //        Operate = param.User.UserName,
                //        Remarks = param.CancelSettlementRemarks
                //    });
                //}

                //////医保执行
                //resultData = _workerMedicalInsuranceRepository.WorkerSettlementCancel(param);
                resultData = param.SettlementJson;
                //取消交易id
                var cancelTransactionId = param.User.TransKey;
                //回参构建
                var xmlData = new HospitalSettlementCancelXml()
                {
                    SettlementNo = param.SettlementNo,
                };
                var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
                var saveXml = new SaveXmlDataParam()
                {
                    User = param.User,
                    MedicalInsuranceBackNum = param.MedicalInsuranceHospitalizationNo,
                    MedicalInsuranceCode = "42",
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
                    CancelSettlementRemarks = param.CancelSettlementRemarks

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
            }
            else if (param.CancelLimit == "2")//取消结算,并删除资料<==>删除资料与取消入院
            {
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

                var updateParamData = new UpdateMedicalInsuranceResidentSettlementParam()
                {
                    UserId = param.User.UserId,
                    Id = param.Id,
                    CancelTransactionId = param.User.TransKey,
                    MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceCancelSettlement,
                    IsHisUpdateState = true
                };
                //更新中间层
                _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParamData);
            }



            
            return resultData;
        }
        /// <summary>
        /// 获取职工生育入院登记参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerBirthHospitalizationRegisterParam GetWorkerBirthHospitalizationRegisterParam(
            BirthHospitalizationRegisterUiParam param)
        {
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var infoData = new GetInpatientInfoParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            };
            //获取病人
            var inpatientData = _serviceBasicService.GetInpatientInfo(infoData);
            if (inpatientData == null) throw new Exception("获取基层病人失败!!!");
            var paramIni = GetWorkerHospitalizationWorkerBirth(inpatientData, param);

            return paramIni;
        }
        /// <summary>
        /// 职工生育入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerBirthHospitalizationRegisterDto WorkerBirthHospitalizationRegister(BirthHospitalizationRegisterUiParam param)
        {
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var infoData = new GetInpatientInfoParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            };
            //获取病人
            var inpatientData = _serviceBasicService.GetInpatientInfo(infoData);
            if (inpatientData == null) throw new Exception("获取基层病人失败!!!");
            var paramIni = GetWorkerHospitalizationWorkerBirth(inpatientData, param);
            var data = JsonConvert.DeserializeObject<WorkerBirthHospitalizationRegisterDto>(param.SettlementJson);
            //var data = _workerMedicalInsuranceRepository.WorkerBirthHospitalizationRegister(paramIni);
            if (data == null) throw new Exception("职工生育入院登记未反馈数据!!!");
            var saveData = new MedicalInsuranceDto
            {
                AdmissionInfoJson = JsonConvert.SerializeObject(paramIni),
                BusinessId = param.BusinessId,
                Id = Guid.NewGuid(),
                IsModify = false,
                InsuranceType = 310,
                MedicalInsuranceState = MedicalInsuranceState.MedicalInsuranceHospitalized,
                MedicalInsuranceHospitalizationNo = data.MedicalInsuranceInpatientNo,
                IsBirthHospital = 1,
                IdentityMark = param.IdentityMark,
                AfferentSign = param.AfferentSign
            };
            ////存中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(userBase, saveData);
            //回参构建
            var xmlData = new HospitalizationRegisterXml()
            {
                MedicalInsuranceType = "1",
                MedicalInsuranceHospitalizationNo = data.MedicalInsuranceInpatientNo,
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
            return data;
        }
        /// <summary>
        /// 获取职工计划生育预结算入参
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerBirthPreSettlementParam GetWorkerBirthPreSettlementParam(WorkerBirthPreSettlementUiParam param)
        {//获取操作人员信息
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
            //获取his结算
            var settlementData = _serviceBasicService.GetHisHospitalizationSettlement(infoData);

            //获取医保病人信息
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (string.IsNullOrWhiteSpace(settlementData.LeaveHospitalDate)) throw new Exception("当前病人在基层中未办理出院,不能办理医保预结算!!!");

            var preSettlement = new WorkerBirthPreSettlementParam()
            {
                MedicalCategory = param.MedicalCategory,
                FetusNumber = param.FetusNumber,
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                LeaveHospitalDate = Convert.ToDateTime(settlementData.LeaveHospitalDate).ToString("yyyyMMdd"),
            };
            //获取诊断
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            preSettlement.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            preSettlement.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            preSettlement.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;

            return preSettlement;
        }
        /// <summary>
        /// 职工生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerBirthPreSettlement(WorkerBirthPreSettlementUiParam param)
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
            //获取his结算
            var settlementData = _serviceBasicService.GetHisHospitalizationSettlement(infoData);

            //获取医保病人信息
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (string.IsNullOrWhiteSpace(settlementData.LeaveHospitalDate)) throw new Exception("当前病人在基层中未办理出院,不能办理医保预结算!!!");

            var preSettlement = new WorkerBirthPreSettlementParam()
            {
                MedicalCategory = param.MedicalCategory,
                FetusNumber = param.FetusNumber,
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                LeaveHospitalDate = Convert.ToDateTime(settlementData.LeaveHospitalDate).ToString("yyyyMMdd"),
            };
            //获取诊断
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            preSettlement.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            preSettlement.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            preSettlement.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;

            var dataIni = JsonConvert.DeserializeObject<WorkerBirthPreSettlementJsonDto>(param.SettlementJson);
            var  resultData = AutoMapper.Mapper.Map<WorkerHospitalizationPreSettlementDto>(dataIni);

            //医保结算
            //var resultData = _workerMedicalInsuranceRepository.WorkerBirthPreSettlement(preSettlement);

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
                Remark = "职工生育住院病人预结算"
            };
            _systemManageRepository.AddHospitalLog(logParam);

            return resultData;
        }
        /// <summary>
        /// 获取职工生育结算入参
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerBirthSettlementParam GetWorkerBirthSettlementParam(WorkerBirthSettlementUiParam param)
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

            var infoParam = new WorkerBirthSettlementParam()
            {
                LeaveHospitalInpatientState = param.LeaveHospitalInpatientState,
                FetusNumber = param.FetusNumber,
                MedicalCategory = param.MedicalCategory,
                Operator = userBase.UserName,
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                LeaveHospitalDate = Convert.ToDateTime(hisSettlement.LeaveHospitalDate).ToString("yyyyMMdd"),

            };
            //获取诊断
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            infoParam.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            infoParam.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            infoParam.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;
            infoParam.AdmissionMainDiagnosis = diagnosisData.DiagnosisDescribe;
            return infoParam;
        }
        /// <summary>
        /// 职工生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public WorkerHospitalizationPreSettlementDto WorkerBirthSettlement(WorkerBirthSettlementUiParam param)
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

            var infoParam = new WorkerBirthSettlementParam()
            {
                LeaveHospitalInpatientState = param.LeaveHospitalInpatientState,
                FetusNumber = param.FetusNumber,
                MedicalCategory = param.MedicalCategory,
                Operator = userBase.UserName,
                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                LeaveHospitalDate = Convert.ToDateTime(hisSettlement.LeaveHospitalDate).ToString("yyyyMMdd"),

            };
            //获取诊断
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            infoParam.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            infoParam.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            infoParam.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;
            infoParam.AdmissionMainDiagnosis = diagnosisData.DiagnosisDescribe;
            var dataIni=  JsonConvert.DeserializeObject<WorkerBirthPreSettlementJsonDto>(param.SettlementJson);
            var  resultData = AutoMapper.Mapper.Map<WorkerHospitalizationPreSettlementDto>(dataIni);

            //// 医保
            //var resultData = _workerMedicalInsuranceRepository.WorkerBirthSettlement(infoParam);
            //报销金额 =统筹支付+补充医疗保险支付金额+专项基金支付金额+
            //公务员补贴+公务员补助+其它支付金额
            decimal reimbursementExpenses = resultData.BasicOverallPay + resultData.SupplementPayAmount + resultData.SpecialFundPayAmount
            + resultData.CivilServantsSubsidies + resultData.CivilServantsSubsidy + resultData.OtherPaymentAmount;
            resultData.ReimbursementExpenses = reimbursementExpenses;
            var updateData = new UpdateMedicalInsuranceResidentSettlementParam()
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
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateData);
            //添加日志
            var logParam = new AddHospitalLogParam()
            {
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = JsonConvert.SerializeObject(resultData),
                User = userBase,
                Remark = "职工住院结算",
                RelationId = residentData.Id,
            };
            //存入基层
            var userInfoData = _residentMedicalInsuranceRepository.GetUserInfo(new ResidentUserInfoParam()
            {
                IdentityMark = residentData.IdentityMark,
                AfferentSign = residentData.AfferentSign,

            });

            // 回参构建
            var xmlData = new HospitalSettlementXml()
            {

                MedicalInsuranceHospitalizationNo = residentData.MedicalInsuranceHospitalizationNo,
                CashPayment = resultData.CashPayment,
                SettlementNo = resultData.DocumentNo,
                PaidAmount = resultData.PaidAmount,
                AllAmount = resultData.TotalAmount,
                PatientName = userInfoData.PatientName,
                AccountBalance = userInfoData.WorkersInsuranceBalance,
                AccountAmountPay = resultData.AccountPayment,
            };
            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);
            var saveXml = new SaveXmlDataParam()
            {
                User = userBase,
                MedicalInsuranceBackNum = resultData.DocumentNo,
                MedicalInsuranceCode = "41",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //结算存基层
            _webBasicRepository.SaveXmlData(saveXml);

            var updateParamData = new UpdateMedicalInsuranceResidentSettlementParam()
            {
                UserId = param.UserId,
                Id = residentData.Id,
                MedicalInsuranceState = MedicalInsuranceState.HisSettlement,
                IsHisUpdateState = true
            };
            //  更新中间层
            _medicalInsuranceSqlRepository.UpdateMedicalInsuranceResidentSettlement(updateParamData);


            //结算后保存信息
            var saveParam = AutoMapper.Mapper.Map<SaveInpatientSettlementParam>(hisSettlement);
            saveParam.Id = (Guid)inpatientInfoData.Id;
            saveParam.User = userBase;
            saveParam.LeaveHospitalDiagnosisJson = JsonConvert.SerializeObject(param.DiagnosisList);
            _hisSqlRepository.SaveInpatientSettlement(saveParam);
            return resultData;
        }
        /// <summary>
        /// 获取生育入院登记入参
        /// </summary>
        /// <param name="inpatientInfo"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private WorkerBirthHospitalizationRegisterParam GetWorkerHospitalizationWorkerBirth
            (InpatientInfoDto inpatientInfo, BirthHospitalizationRegisterUiParam param)
        {
            var iniParam = new WorkerBirthHospitalizationRegisterParam()
            {
                IdentityMark = param.IdentityMark,
                AfferentSign = param.AfferentSign,
                MedicalCategory = param.MedicalCategory,
                FetusNumber =Convert.ToInt16(param.FetusNumber),
                SpouseIdCardNo = param.SpouseIdCardNo,
                SpouseName = param.SpouseName,
                AdmissionDate = Convert.ToDateTime(inpatientInfo.AdmissionDate).ToString("yyyyMMdd"),
                HospitalizationNo = inpatientInfo.HospitalizationNo,
                Operators = inpatientInfo.AdmissionOperator,
                InpatientDepartmentCode = inpatientInfo.InDepartmentName,
            };
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            iniParam.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            iniParam.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            iniParam.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;
            iniParam.AdmissionMainDiagnosis = diagnosisData.DiagnosisDescribe;
            return iniParam;
        }

    }
}
