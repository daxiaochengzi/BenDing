using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.HisXml;
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
   public class MedicalInsuranceService: IMedicalInsuranceService
    {
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsurance;
        private readonly IWebBasicRepository _webServiceBasic;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IWebServiceBasicService _serviceBasicService;
        private readonly IWorkerMedicalInsuranceService _workerMedicalInsuranceService;
        private readonly IResidentMedicalInsuranceService _residentMedicalInsuranceService;

        public MedicalInsuranceService(
            IResidentMedicalInsuranceRepository insuranceRepository,
            IWebBasicRepository iWebServiceBasic,
            IHisSqlRepository hisSqlRepository,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            ISystemManageRepository systemManageRepository,
            IWebServiceBasicService serviceBasicService,
            IWorkerMedicalInsuranceService workerMedicalInsuranceService,
            IResidentMedicalInsuranceService residentMedicalInsuranceService
        )
        {
            _residentMedicalInsurance = insuranceRepository;
            _webServiceBasic = iWebServiceBasic;
            _hisSqlRepository = hisSqlRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _systemManageRepository = systemManageRepository;
            _serviceBasicService = serviceBasicService;
            _workerMedicalInsuranceService = workerMedicalInsuranceService;
            _residentMedicalInsuranceService = residentMedicalInsuranceService;
        }

        public void HospitalizationRegister(ResidentHospitalizationRegisterUiParam param)
        {
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            userBase.TransKey = param.TransKey;
            var infoData = new GetInpatientInfoParam()
            {
                User = userBase,
                BusinessId = param.BusinessId,
            };
            //获取医保病人
            var inpatientData = _serviceBasicService.GetInpatientInfo(infoData);
            if (inpatientData == null) throw new Exception("获取基层住院病人失败");
            //医保登录
            _residentMedicalInsurance.Login(new QueryHospitalOperatorParam() { UserId = param.UserId });

            if (param.InsuranceType == "342")
            {
                var residentParam = GetResidentHospitalizationRegisterParam(param, inpatientData);
                _residentMedicalInsuranceService.HospitalizationRegister(residentParam, userBase);
            }
            //职工
            if (param.InsuranceType == "310")
            {
                var workerParam = GetWorkerHospitalizationRegisterParam(param, inpatientData, userBase);
                _workerMedicalInsuranceService.WorkerHospitalizationRegister(workerParam);
            }
            infoData.IsSave = true;
            //保存病人信息
            _serviceBasicService.GetInpatientInfo(infoData);
        }
        /// <summary>
        /// 入院登记修改
        /// </summary>
        /// <param name="param"></param>
      
        public void HospitalizationModify(HospitalizationModifyUiParam param)
        { //获取操作人员信息
            var userBase = _serviceBasicService.GetUserBaseInfo(param.UserId);
            var queryResidentParam = new QueryMedicalInsuranceResidentInfoParam()
            {
                BusinessId = param.BusinessId,
                OrganizationCode = userBase.OrganizationCode
            };
            userBase.TransKey = param.TransKey;

            //获取医保病人信息
            var residentData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(queryResidentParam);
            if (residentData==null) throw  new  Exception("当前病人未办理医保入院");
           //职工
            if (residentData.InsuranceType == "310")
            {
            }
            //居民
            if (residentData.InsuranceType == "342")
            {
                var residentParam = GetResidentHospitalizationModify(param);
                _residentMedicalInsuranceService.HospitalizationModify(residentParam, userBase);
            }

        }
        /// <summary>
        /// 入院登记修改
        /// </summary>
        /// <param name="param"></param>
        public HospitalizationModifyParam GetResidentHospitalizationModify(HospitalizationModifyUiParam param)
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
            iniParam.InpatientDepartmentCode = paramDto.InDepartmentName;
            iniParam.BedNumber = paramDto.AdmissionBed;
            iniParam.HospitalizationNo = paramDto.HospitalizationNo;
            iniParam.Operators = paramDto.AdmissionOperator;
            iniParam.InsuranceType = param.InsuranceType;
            iniParam.BusinessId = param.BusinessId;
            return iniParam;
        }

        /// <summary>
        /// 获取职工入院登记入参
        /// </summary>
        /// <param name="param"></param>
        /// <param name="paramDto"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private WorKerHospitalizationRegisterParam GetWorkerHospitalizationRegisterParam(
            ResidentHospitalizationRegisterUiParam param, InpatientInfoDto paramDto, UserInfoDto user)
        {
            var iniParam = new WorKerHospitalizationRegisterParam();
            var diagnosisData = CommonHelp.GetDiagnosis(param.DiagnosisList);
            iniParam.AdmissionMainDiagnosisIcd10 = diagnosisData.AdmissionMainDiagnosisIcd10;
            iniParam.DiagnosisIcd10Two = diagnosisData.DiagnosisIcd10Two;
            iniParam.DiagnosisIcd10Three = diagnosisData.DiagnosisIcd10Three;
            //获取医院等级
            var gradeData = _systemManageRepository.QueryHospitalOrganizationGrade(user.OrganizationCode);
            if (gradeData == null) throw new Exception("获取医院等级失败!!!");
            if (string.IsNullOrWhiteSpace(gradeData.AdministrativeArea)) throw new Exception("当前医院未设置行政区域!!!");
            iniParam.IdentityMark = param.IdentityMark;
            iniParam.AfferentSign = param.AfferentSign;
            iniParam.MedicalCategory = param.MedicalCategory;
            iniParam.AdmissionDate = Convert.ToDateTime(paramDto.AdmissionDate).ToString("yyyyMMdd");
            iniParam.InpatientDepartmentCode = paramDto.InDepartmentName;
            iniParam.BedNumber = paramDto.AdmissionBed;
            iniParam.HospitalizationNo = paramDto.HospitalizationNo;
            iniParam.Operators = paramDto.AdmissionOperator;
            iniParam.BusinessId = param.BusinessId;
            iniParam.AdministrativeArea = gradeData.AdministrativeArea;
            iniParam.InpatientArea = paramDto.AdmissionWard;
            var userData = _systemManageRepository.QueryHospitalOperator(
                new QueryHospitalOperatorParam() { UserId = param.UserId });
            iniParam.OrganizationCode = userData.MedicalInsuranceAccount;
            return iniParam;
        }
    }
}
