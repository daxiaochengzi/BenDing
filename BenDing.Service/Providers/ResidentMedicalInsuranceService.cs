using System;
using System.Linq;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Repository.Interfaces.Web;
using BenDing.Service.Interfaces;

namespace BenDing.Service.Providers
{
    public class ResidentMedicalInsuranceService : IResidentMedicalInsuranceService
    {
        private readonly IResidentMedicalInsuranceRepository _residentMedicalInsurance;
        private readonly IWebBasicRepository _webServiceBasic;
        private readonly IHisSqlRepository _hisSqlRepository;
        private readonly IMedicalInsuranceSqlRepository _medicalInsuranceSqlRepository;

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
            _residentMedicalInsurance.HospitalizationRegister(param, user);


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
