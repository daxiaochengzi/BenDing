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
       /// 
       /// </summary>
       /// <param name="param"></param>
       /// <returns></returns>
        Task<Int64> ProjectDownloadCount(ResidentProjectDownloadParam param);
        /// <summary>
        /// 修改居民入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task HospitalizationModify(HospitalizationModifyParam param, UserInfoDto user);
        /// <summary>
        /// 医保登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        Task Login(QueryHospitalOperatorParam param);

        /// <summary>
        /// 处方上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<RetrunPrescriptionUploadDto> PrescriptionUpload(PrescriptionUploadUiParam param, UserInfoDto user);
        /// <summary>
        /// 删除处方数据
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ids"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task DeletePrescriptionUpload(DeletePrescriptionUploadParam param, List<Guid> ids, UserInfoDto user);

            /// <summary>
            /// 费用预结算
            /// </summary>
            /// <param name="param"></param>
            /// <returns></returns>
            Task<HospitalizationPresettlementDto> HospitalizationPresettlement(HospitalizationPresettlementParam param, UserInfoDto user);
        /// <summary>
        /// 医保出院结算信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="InfoParam"></param>
        /// <returns></returns>
        Task<HospitalizationPresettlementDto> LeaveHospitalSettlement(LeaveHospitalSettlementParam param, LeaveHospitalSettlementInfoParam InfoParam);
        /// <summary>
        /// 查询医保出院结算信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<HospitalizationPresettlementDto> QueryLeaveHospitalSettlement(QueryLeaveHospitalSettlementParam param);
        /// <summary>
        ///	医保处方明细查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<QueryPrescriptionDetailDto> QueryPrescriptionDetail(QueryPrescriptionDetailParam param);
        /// <summary>
        /// 出院院结算取消
        /// </summary>
        /// <param name="param"></param>
        /// <param name="infoParam"></param>
        /// <returns></returns>
        Task LeaveHospitalSettlementCancel(LeaveHospitalSettlementCancelParam param, LeaveHospitalSettlementCancelInfoParam infoParam);

    }
}
