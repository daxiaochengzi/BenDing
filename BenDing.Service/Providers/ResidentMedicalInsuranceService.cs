using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;

namespace BenDing.Service.Providers
{
   public class ResidentMedicalInsuranceService: IResidentMedicalInsuranceService
    {
        private IResidentMedicalInsuranceRepository _residentMedicalInsurance;
        private IWebBasicRepository _webServiceBasic;
        private IHisSqlRepository _hisSqlRepository;
        private IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;

        public ResidentMedicalInsuranceService(
            IResidentMedicalInsuranceRepository insuranceRepository,
            IWebBasicRepository iWebServiceBasic,
            IHisSqlRepository hisSqlRepository,
            IMedicalInsuranceSqlRepository medicalInsuranceSqlRepository
            )
        {
            _residentMedicalInsurance = insuranceRepository;
            _webServiceBasic = iWebServiceBasic;
            _hisSqlRepository = hisSqlRepository;
            _medicalInsuranceSqlRepository = medicalInsuranceSqlRepository;
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
        //处方自动上传
        public async Task PrescriptionUploadAutomatic(PrescriptionUploadAutomaticParam param, UserInfoDto user)
        {//获取所有未传费用病人
            var allPatients=  await _hisSqlRepository.QueryAllHospitalizationPatients(param);
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
                            BusinessId=items.BusinessId
                        };
                        await _residentMedicalInsurance.PrescriptionUpload(uploadParam, user);
                    }

                }
            }
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
            var updateTime=await _medicalInsuranceSqlRepository.ProjectDownloadTimeMax();
            if (!string.IsNullOrWhiteSpace(updateTime)) param.UpdateTime = Convert.ToDateTime(updateTime).ToString("yyyyMMdd");

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
                    await _medicalInsuranceSqlRepository.ProjectDownload(new UserInfoDto() { UserId = param.UserId }, data.Row);
                }
                i ++;
            }
            resultData = "下载成功共" + allNum + "条记录";
            return resultData;
        }

       
    }
}
