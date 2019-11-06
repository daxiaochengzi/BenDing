using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Repository.Interfaces.Web
{
  public  interface IResidentMedicalInsuranceRepository
    {/// <summary>
        /// 获取个人基础资料
        /// </summary>
        /// <param name="param"></param>
        Task<ResidentUserInfoDto> GetUserInfo(ResidentUserInfoParam param);
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        Task HospitalizationRegister(ResidentHospitalizationRegisterParam param, UserInfoDto user);
        /// <summary>
        /// 项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        Task<ResidentProjectDownloadDto> ProjectDownload(ResidentProjectDownloadParam param);
        /// <summary>
        /// 修改居民入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task HospitalizationModify(HospitalizationModifyParam param, UserInfoDto user);

        Task Login(QueryHospitalOperatorParam param);

        /// <summary>
        /// 处方上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task PrescriptionUpload(PrescriptionUploadUiParam param, UserInfoDto user);
    }
}
