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
        private IDataBaseHelpRepository _baseHelpRepository;

        public ResidentMedicalInsuranceService(
            IResidentMedicalInsuranceRepository insuranceRepository,
            IWebBasicRepository iWebServiceBasic,
            IDataBaseHelpRepository iBaseHelpRepository
            )
        {
            _residentMedicalInsurance = insuranceRepository;
            _webServiceBasic = iWebServiceBasic;
            _baseHelpRepository = iBaseHelpRepository;
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
        /// <summary>
        /// 医保项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> ProjectDownload(ResidentProjectDownloadParam param)
        {
            ResidentProjectDownloadDto data;
            var pageIni = param.Page;
            string resultData = "";
            param.QueryType = 1;
            var count = await _residentMedicalInsurance.ProjectDownloadCount(param);
            var cnt = Convert.ToInt32(count / param.Limit) + ((count % param.Limit) > 0 ? 1 : 0);
            param.QueryType = 2;
            Int64 allNum = 0;
            var i = 0;
            while (i < cnt)
            {
                param.Page = i + pageIni;
                data = await _residentMedicalInsurance.ProjectDownload(param);
                if (data != null && data.Row.Any())
                {
                    allNum += data.Row.Count;
                    await _baseHelpRepository.ProjectDownload(new UserInfoDto() { UserId = param.UserId }, data.Row);
                }
                i ++;
            }
            resultData = "下载成功共" + allNum + "条记录";
            return resultData;
        }

       
    }
}
