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
        ResidentUserInfoDto GetUserInfo(ResidentUserInfoParam param);
        /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        void HospitalizationRegister(ResidentHospitalizationRegisterParam param, UserInfoDto user);
        /// <summary>
        /// 项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        string ProjectDownload(ResidentProjectDownloadParam param);
        void PrescriptionUploadAutomatic(PrescriptionUploadAutomaticParam param, UserInfoDto user);


    }
}
