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
    public interface IBaseSqlServerRepository
    {
        /// <summary>
        /// 医保病人信息保存
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Int32> SaveMedicalInsuranceResidentInfo(MedicalInsuranceResidentInfoParam param);

        /// <summary>
        /// 医保病人信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<MedicalInsuranceResidentInfoDto> QueryMedicalInsuranceResidentInfo(
            QueryMedicalInsuranceResidentInfoParam param);

        /// <summary>
        ///  更新医保病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<int> UpdateMedicalInsuranceResidentInfo(
            UpdateMedicalInsuranceResidentInfoParam param);

        /// <summary>
        /// 住院病人明细查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<QueryInpatientInfoDetailDto>> InpatientInfoDetailQuery(InpatientInfoDetailQueryParam param);

        /// <summary>
        /// 单病种下载
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Int32> SingleResidentInfoDownload(UserInfoDto user, List<SingleResidentInfoDto> param);
        /// <summary>
        /// 医保对码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task MedicalInsurancePairCode(MedicalInsurancePairCodesUiParam param);
        /// <summary>
        /// 医保对码查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
       Task<List<QueryMedicalInsurancePairCodeDto>> QueryMedicalInsurancePairCode(
            QueryMedicalInsurancePairCodeParam param);
        /// <summary>
        /// 目录对照管理
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        Task<Dictionary<int, List<DirectoryComparisonManagementDto>>> DirectoryComparisonManagement(
            DirectoryComparisonManagementUiParam param);
      

         Task<List<QueryThreeCataloguePairCodeUploadDto>> ThreeCataloguePairCodeUpload(
            string organizationCode);
        /// <summary>
        /// 三大目录对码
        /// </summary>
        /// <param name="organizationCode"></param>
        /// <returns></returns>
        Task<int> UpdateThreeCataloguePairCodeUpload(string organizationCode);

        //Task<int> AddProjectBatch(AddProjectBatchParam param);
        /// <summary>
        /// 更新批次号
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<int> UpdateHospitalizationFee(List<UpdateHospitalizationFeeParam> param, UserInfoDto user);


    }

}
