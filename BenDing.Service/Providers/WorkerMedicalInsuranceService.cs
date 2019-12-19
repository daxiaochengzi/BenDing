using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Dto.Workers;
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
                    InsuranceType = 310,
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
                //存中间库
                 saveData.IsDelete = false;
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
                Id= param.Id,
                IsModify = true,
            };
            //存中间库
            _medicalInsuranceSqlRepository.SaveMedicalInsurance(param.User, saveData);
        }
    }
}
