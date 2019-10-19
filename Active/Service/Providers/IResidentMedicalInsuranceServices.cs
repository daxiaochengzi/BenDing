using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Model.Dto;
using BenDingActive.Model.Params;

namespace BenDingActive.Service.Providers
{
  public  interface IResidentMedicalInsuranceServices
    {    /// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        Task<UserInfoDto> GetUserInfo(ActiveUserInfoParam param);
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        Task<HospitalizationRegisterDto> HospitalizationRegister(HospitalizationRegisterParam param);
        /// <summary>
        /// 项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

         Task<ProjectDownloadDto> ProjectDownload(ProjectDownloadParam param);
    }
}
