using System;
using System.Linq;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
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
    public class ResidentMedicalInsuranceService : IResidentMedicalInsuranceService
    {
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsurance;
        private readonly IWebBasicRepository _webServiceBasic;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;
        private readonly ISystemManageRepository _systemManageRepository;
        private readonly IWebServiceBasicService _serviceBasicService;
        private readonly IWorkerMedicalInsuranceService _workerMedicalInsuranceService;

        public ResidentMedicalInsuranceService(
            IResidentMedicalInsuranceRepository insuranceRepository,
            IWebBasicRepository iWebServiceBasic,
            IHisSqlRepository hisSqlRepository,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository,
            ISystemManageRepository systemManageRepository,
            IWebServiceBasicService serviceBasicService,
            IWorkerMedicalInsuranceService workerMedicalInsuranceService
            )
        {
            _residentMedicalInsurance = insuranceRepository;
            _webServiceBasic = iWebServiceBasic;
            _hisSqlRepository = hisSqlRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
            _systemManageRepository = systemManageRepository;
            _serviceBasicService = serviceBasicService;
            _workerMedicalInsuranceService = workerMedicalInsuranceService;
        }
        public ResidentUserInfoDto GetUserInfo(ResidentUserInfoParam param)
        {
            var resultData = _residentMedicalInsurance.GetUserInfo(param);

            return resultData;
        }

        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public void HospitalizationRegister(ResidentHospitalizationRegisterParam param, UserInfoDto user)
        {
            var residentData = _residentMedicalInsurance.HospitalizationRegister(param);
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
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(user, saveData);
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
                User = user,
                MedicalInsuranceBackNum = "CXJB002",
                MedicalInsuranceCode = "21",
                BusinessId = param.BusinessId,
                BackParam = strXmlBackParam
            };
            //存基层
            _serviceBasicService.SaveXmlData(saveXml);
            //更新中间库
            saveData.MedicalInsuranceState = MedicalInsuranceState.HisHospitalized;
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(user, saveData);
            //日志
            var logParam = new AddHospitalLogParam
            {
                User = user,
                RelationId = saveData.Id,
                JoinOrOldJson = JsonConvert.SerializeObject(param),
                ReturnOrNewJson = JsonConvert.SerializeObject(residentData),
                Remark = "医保入院登记;TransactionId:" + user.TransKey
            };
            _systemManageRepository.AddHospitalLog(logParam);

        }
       
     
      

        /// <summary>
        /// 入院登记修改
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user"></param>
        public void HospitalizationModify(HospitalizationModifyParam param, UserInfoDto user)
        {
            _residentMedicalInsurance.HospitalizationModify(param, user);
            var strXmlIntoParam = XmlSerializeHelper.XmlParticipationParam();
            //回参构建
            var xmlData = new HospitalizationRegisterXml()
            {

                MedicalInsuranceHospitalizationNo = param.MedicalInsuranceHospitalizationNo,
            };
            var strXmlBackParam = XmlSerializeHelper.HisXmlSerialize(xmlData);

            var saveXmlData = new SaveXmlData();
            saveXmlData.OrganizationCode = user.OrganizationCode;
            saveXmlData.AuthCode = user.AuthCode;
            saveXmlData.BusinessId = param.BusinessId;
            saveXmlData.TransactionId = param.TransKey;
            saveXmlData.MedicalInsuranceBackNum = "CXJB003";
            saveXmlData.BackParam = CommonHelp.EncodeBase64("utf-8", strXmlBackParam);
            saveXmlData.IntoParam = CommonHelp.EncodeBase64("utf-8", strXmlIntoParam);
            saveXmlData.MedicalInsuranceCode = "23";
            saveXmlData.UserId = user.UserId;
            _webServiceBasic.HIS_InterfaceList("38", JsonConvert.SerializeObject(saveXmlData));
            var paramStr = "";
            var queryData = _medicalInsuranceSqlRepository.QueryMedicalInsuranceResidentInfo(
                new QueryMedicalInsuranceResidentInfoParam
                {
                    BusinessId = param.BusinessId,
                    OrganizationCode = user.OrganizationCode
                });
            if (!string.IsNullOrWhiteSpace(queryData.AdmissionInfoJson))
            {
                var data =
                    JsonConvert.DeserializeObject<QueryMedicalInsuranceDetailDto>(queryData
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
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(user, saveData);
            //日志
            var logParam = new AddHospitalLogParam();
            logParam.User = user;
            logParam.RelationId = queryData.Id;
            logParam.JoinOrOldJson = queryData.AdmissionInfoJson;
            logParam.ReturnOrNewJson = paramStr;
            logParam.Remark = "医保入院登记修改";
            _systemManageRepository.AddHospitalLog(logParam);
        }

        //处方自动上传
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
                        _residentMedicalInsurance.PrescriptionUpload(uploadParam, user);
                    }

                }
            }
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

            var count = _residentMedicalInsurance.ProjectDownloadCount(param);
            var cnt = Convert.ToInt32(count / param.Limit) + ((count % param.Limit) > 0 ? 1 : 0);
            param.QueryType = 2;
            Int64 allNum = 0;
            var i = 0;
            while (i < cnt)
            {
                param.Page = i + pageIni;
                data = _residentMedicalInsurance.ProjectDownload(param);
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


    }
}
