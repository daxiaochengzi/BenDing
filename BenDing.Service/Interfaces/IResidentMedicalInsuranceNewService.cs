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

namespace BenDing.Service.Interfaces
{  /// <summary>
  /// 居民插件模式
  /// </summary>
    public  interface  IResidentMedicalInsuranceNewService
    {
        /// <summary>
        /// 获取居民入院登记参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ResidentHospitalizationRegisterParam GetResidentHospitalizationRegisterParam(
            ResidentHospitalizationRegisterUiParam param);

        /// <summary>
        /// 居民入院登记
        /// </summary>
        /// <param name="param"></param>
        void HospitalizationRegister(ResidentHospitalizationRegisterUiParam param);

        /// <summary>
        /// 获取居民入院登记修改参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
         HospitalizationModifyParam GetHospitalizationModifyParam(HospitalizationModifyUiParam param);

        /// <summary>
        /// 修改居民入院登记
        /// </summary>
        /// <param name="param"></param>
         void HospitalizationModify(HospitalizationModifyUiParam param);
        /// <summary>
        /// 获取预结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        HospitalizationPresettlementParam GetHospitalizationPreSettlement(
            HospitalizationPreSettlementUiParam param);
        /// <summary>
        /// 居民住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        HospitalizationPresettlementDto HospitalizationPreSettlement(HospitalizationPreSettlementUiParam param);

        /// <summary>
        /// 获取居民出院结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        LeaveHospitalSettlementParam GetHospitalizationSettlement(LeaveHospitalSettlementUiParam param);
        /// <summary>
        /// 居民住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        HospitalizationPresettlementDto LeaveHospitalSettlement(LeaveHospitalSettlementUiParam param);
        /// <summary>
        /// 获取处方上传参数
        /// </summary>
        /// <param name="param"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        GetPrescriptionUploadParam GetPrescriptionUploadParam(PrescriptionUploadUiParam param, UserInfoDto user);
        /// <summary>
        /// 处方数据更新
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int PrescriptionUploadUpdateData(PrescriptionUploadUpdateDataParam param);

        /// <summary>
        /// 取消医保出院结算
        /// </summary>
        /// <param name="param"></param>
        /// <param name="infoParam"></param>
        /// <returns></returns>
        void LeaveHospitalSettlementCancel(LeaveHospitalSettlementCancelParam param,
            LeaveHospitalSettlementCancelInfoParam infoParam);
        /// <summary>
        /// 获取处方取消上传参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        DeletePrescriptionUploadParam GetDeletePrescriptionUploadParam(BaseUiBusinessIdDataParam param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        void  DeletePrescriptionUpload(BaseUiBusinessIdDataParam param);

    }
}
