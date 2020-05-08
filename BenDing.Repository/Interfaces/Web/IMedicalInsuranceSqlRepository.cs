using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Repository.Interfaces.Web
{/// <summary>
/// 医保
/// </summary>
    public interface IMedicalInsuranceSqlRepository
    {
        #region 备用
        ///// <summary>
        ///// 医保病人信息保存
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //<Int32> SaveMedicalInsuranceResidentInfo(MedicalInsuranceResidentInfoParam param);
        ///// <summary>
        /////  更新医保病人信息
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //<int> UpdateMedicalInsuranceResidentInfo(
        //    UpdateMedicalInsuranceResidentInfoParam param);
        ///// <summary>
        ///// 单病种下载
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //<Int32> SingleResidentInfoDownload(UserInfoDto user, List<SingleResidentInfoDto> param);

        #endregion
        int UpdateMedicalInsuranceResidentSettlement(UpdateMedicalInsuranceResidentSettlementParam param);
        /// <summary>
        /// 医保中心端查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Dictionary<int, List<ResidentProjectDownloadRow>> QueryProjectDownload(QueryProjectUiParam param);
        /// <summary>
        /// 下载医保项目 DownloadD
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Int32 ProjectDownload(UserInfoDto user, List<ResidentProjectDownloadRowDataRowDto> param);
        /// <summary>
        /// 获取医保项目更新时间
        /// </summary>
        /// <returns></returns>
        string ProjectDownloadTimeMax();
        /// <summary>
        /// 医保信息保存
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        void SaveMedicalInsurance(UserInfoDto user, MedicalInsuranceDto param);
        /// <summary>
        /// 医保病人信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        MedicalInsuranceResidentInfoDto QueryMedicalInsuranceResidentInfo(
            QueryMedicalInsuranceResidentInfoParam param);
        /// <summary>
        /// 医保对码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        void MedicalInsurancePairCode(MedicalInsurancePairCodesUiParam param);

        /// <summary>
        /// 医保对码查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<QueryMedicalInsurancePairCodeDto> QueryMedicalInsurancePairCode(
             QueryMedicalInsurancePairCodeParam param);
        /// <summary>
        /// 目录对照管理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        Dictionary<int, List<DirectoryComparisonManagementDto>> DirectoryComparisonManagement(
           DirectoryComparisonManagementUiParam param);
        List<QueryThreeCataloguePairCodeUploadDto> ThreeCataloguePairCodeUpload(UpdateThreeCataloguePairCodeUploadParam param);
        /// <summary>
        /// 三大目录对码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int UpdateThreeCataloguePairCodeUpload(UpdateThreeCataloguePairCodeUploadParam param);

         void HospitalThreeCatalogBatchUpload(UserInfoDto user);
        /// <summary>
        /// 更新批次号
        /// </summary>
        /// <param name="param"></param>
        /// <param name="batchConfirmFail">是否确认失败</param>
        /// <param name="user"></param>
        /// <returns></returns>
        int UpdateHospitalizationFee(List<UpdateHospitalizationFeeParam> param, bool batchConfirmFail, UserInfoDto user);


    }

}
