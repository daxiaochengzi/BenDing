using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;

namespace BenDing.Service.Interfaces
{
   public interface IResidentMedicalInsuranceService
    {/// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        Task<ResidentUserInfoDto> GetUserInfo(ResidentUserInfoParam param);
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        Task<ResidentHospitalizationRegisterDto> HospitalizationRegister(ResidentHospitalizationRegisterParam param);
        /// <summary>
        /// 项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        Task<string> ProjectDownload(ResidentProjectDownloadParam param);
    }
}
