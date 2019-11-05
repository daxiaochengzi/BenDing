using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;

namespace BenDing.Service.Providers
{
   public class ResidentMedicalInsuranceService: IResidentMedicalInsuranceService
    {
        private IResidentMedicalInsuranceRepository _residentMedicalInsurance;
        private IWebBasicRepository _webServiceBasic;

        public ResidentMedicalInsuranceService(
            IResidentMedicalInsuranceRepository insuranceRepository,
            IWebBasicRepository iWebServiceBasic
            )
        {
            _residentMedicalInsurance = insuranceRepository;
            _webServiceBasic = iWebServiceBasic;
        }
        public async Task<ResidentUserInfoDto> GetUserInfo(ResidentUserInfoParam param)
        {
            var resultData = await _residentMedicalInsurance.GetUserInfo(param);

            return resultData;
        }

        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        public async Task HospitalizationRegister(ResidentHospitalizationRegisterParam param, UserInfoDto user)
        {
            await _residentMedicalInsurance.HospitalizationRegister(param, user);

           
        }

        public async Task<string> ProjectDownload(ResidentProjectDownloadParam param)
        {
            var resultData=new ResidentProjectDownloadDto();
            resultData = await _residentMedicalInsurance.ProjectDownload(param);

            return "123";
        }

       
    }
}
