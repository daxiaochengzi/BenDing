using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Repository.Interfaces.Web
{
   public interface IDataBaseHelpRepository
    {
        Task ChangeOrg(UserInfoDto userInfo, List<OrgDto> param);
        /// <summary>
        /// 获取三大目录更新时间
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        Task<string> GetTime(int num);
        /// <summary>
        /// 删除三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<int> DeleteCatalog(UserInfoDto user, int param);

        /// <summary>
        /// 医保信息查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Int32> QueryMedicalInsurance(string param);

        /// <summary>
        /// 删除医保信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Int32> DeleteMedicalInsurance(UserInfoDto user, string param);
        /// <summary>
        /// 获取HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<Int32> SaveInformationInfo(UserInfoDto user, List<InformationDto> param, InformationParam info);
        Task<List<QueryInformationInfoDto>> QueryInformationInfo(InformationParam param);
        /// <summary>
        /// 获取门诊病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task GetOutpatientPerson(UserInfoDto user, List<OutpatientInfoDto> param);
        /// <summary>
        /// 获取门诊病人明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task GetOutpatientDetailPerson(UserInfoDto user, List<OutpatientDetailDto> param);
        /// <summary>
        /// 基层端三大目录查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Dictionary<int, List<QueryCatalogDto>>> QueryCatalog(QueryCatalogUiParam param);
        /// <summary>
        /// 添加三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        Task AddCatalog(UserInfoDto user, List<CatalogDto> param, CatalogTypeEnum type);

        /// <summary>
        /// 保存住院病人明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task SaveInpatientInfoDetail(UserInfoDto user, List<InpatientInfoDetailDto> param);
        /// <summary>
        /// 获取所有未传费用的住院病人
        /// </summary>
        /// <returns></returns>
        Task<List<QueryAllHospitalizationPatientsDto>> QueryAllHospitalizationPatients(PrescriptionUploadAutomaticParam param);
        /// <summary>
        /// 住院清单查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        Task<Dictionary<int, List<QueryHospitalizationFeeDto>>> QueryHospitalizationFee(
            QueryHospitalizationFeeUiParam param);

        /// <summary>
        /// ICD10Time 
        /// </summary>
        /// <returns></returns>
        Task<string> GetICD10Time();
        /// <summary>
        /// ICD10Time添加
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task AddICD10(List<ICD10InfoDto> param, UserInfoDto user);
        /// <summary>
        /// ICD10查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<List<QueryICD10InfoDto>> QueryICD10(QueryICD10UiParam param);
        /// <summary>
        /// 保存住院病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task GetInpatientInfo(UserInfoDto user, InpatientInfoDto param);
        /// <summary>
        /// 医保信息保存
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task SaveMedicalInsurance(UserInfoDto user, MedicalInsuranceDto param);
        /// <summary>
        /// 医保反馈数据保存
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task SaveMedicalInsuranceDataAll(MedicalInsuranceDataAllParam param);
        /// <summary>
        /// 医保反馈数据查询保存
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<MedicalInsuranceDataAllDto> SaveMedicalInsuranceDataAllQuery(
            MedicalInsuranceDataAllParamUIQueryParam param);
        /// <summary>
        /// 下载医保项目 DownloadD
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Int32> ProjectDownload(UserInfoDto user, List<ResidentProjectDownloadRowDataRowDto> param);
        /// <summary>
        /// 获取医保项目更新时间
        /// </summary>
        /// <returns></returns>
       Task<string> ProjectDownloadTimeMax();
       /// <summary>
       ///  医保项目下载查询
       /// </summary>
       /// <param name="param"></param>
       /// <returns></returns>
       Task<Dictionary<int, List<ResidentProjectDownloadRow>>> QueryProjectDownload(
            QueryProjectUiParam param);
        /// <summary>
        /// 病人医保信息查询
        /// </summary>
        /// <param name="user"></param>
        /// <param name="businessId"></param>
        /// <returns></returns>
       Task<QueryMedicalInsuranceDto> QueryMedicalInsurance(UserInfoDto user, string businessId);
    }
}
