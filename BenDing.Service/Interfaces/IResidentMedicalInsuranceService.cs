using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;

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
         void HospitalizationRegister(ResidentHospitalizationRegisterUiParam param);

        /// <summary>
        /// 修改入院登记
        /// </summary>
        /// <param name="param"></param>
         void HospitalizationModify(HospitalizationModifyUiParam param);
        /// <summary>
        /// 居民住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        HospitalizationPresettlementDto LeaveHospitalSettlement(LeaveHospitalSettlementUiParam param);
        /// <summary>
        /// 项目下载
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        string ProjectDownload(ResidentProjectDownloadParam param);
        void PrescriptionUploadAutomatic(PrescriptionUploadAutomaticParam param, UserInfoDto user);
        /// <summary>
        /// 居民住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
         HospitalizationPresettlementDto HospitalizationPreSettlement(UiBaseDataParam param);
        /// <summary>
        /// 处方删除
        /// </summary>
        /// <param name="param"></param>
         void DeletePrescriptionUpload(BaseUiBusinessIdDataParam param);
        //医保登陆
        void Login(QueryHospitalOperatorParam param);

    }
}
