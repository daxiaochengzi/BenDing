using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Enums;
using BenDing.Domain.Models.Params.Resident;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Repository.Interfaces.Web
{
    public interface IHisSqlRepository
    {
        void ChangeOrg(UserInfoDto userInfo, List<OrgDto> param);
        /// <summary>
        /// 获取三大目录更新时间
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        string GetTime(int num);
        /// <summary>
        /// 删除三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int DeleteCatalog(UserInfoDto user, int param);
        /// <summary>
        /// 删除医保信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Int32 DeleteMedicalInsurance(UserInfoDto user, string param);
        /// <summary>
        /// 获取HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        Int32 SaveInformationInfo(UserInfoDto user, List<InformationDto> param, InformationParam info);
        List<QueryInformationInfoDto> QueryInformationInfo(InformationParam param);
        /// <summary>
        /// 获取门诊病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        void SaveOutpatient(UserInfoDto user, BaseOutpatientInfoDto param);
        /// <summary>
        /// 查询门诊病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        QueryOutpatientDto QueryOutpatient(QueryOutpatientParam param);

        /// <summary>
        /// 保存门诊病人明细
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        void SaveOutpatientDetail(UserInfoDto user, List<BaseOutpatientDetailDto> param);
        /// <summary>
        /// 基层端三大目录查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Dictionary<int, List<QueryCatalogDto>> QueryCatalog(QueryCatalogUiParam param);
        /// <summary>
        /// 添加三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        void AddCatalog(UserInfoDto user, List<CatalogDto> param, CatalogTypeEnum type);

        /// <summary>
        /// 保存住院病人明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        void SaveInpatientInfoDetail(UserInfoDto user, List<InpatientInfoDetailDto> param);
        /// <summary>
        /// 获取所有未传费用的住院病人
        /// </summary>
        /// <returns></returns>
        List<QueryAllHospitalizationPatientsDto> QueryAllHospitalizationPatients(PrescriptionUploadAutomaticParam param);
        /// <summary>
        /// 住院病人明细查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<QueryInpatientInfoDetailDto> InpatientInfoDetailQuery(InpatientInfoDetailQueryParam param);
        /// <summary>
        /// 住院清单查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>

        Dictionary<int, List<QueryHospitalizationFeeDto>> QueryHospitalizationFee(
            QueryHospitalizationFeeUiParam param);

        /// <summary>
        /// ICD10Time 
        /// </summary>
        /// <returns></returns>
        string GetICD10Time();
        /// <summary>
        /// ICD10Time添加
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        void AddICD10(List<ICD10InfoDto> param, UserInfoDto user);
        /// <summary>
        /// ICD10查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<QueryICD10InfoDto> QueryICD10(QueryICD10UiParam param);
        /// <summary>
        /// 保存住院病人信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        void SaveInpatientInfo(UserInfoDto user, InpatientInfoDto param);
        /// <summary>
        /// 住院病人查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        QueryInpatientInfoDto QueryInpatientInfo(QueryInpatientInfoParam param);




    }
}
