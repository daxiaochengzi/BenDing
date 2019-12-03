using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BenDing.Domain.Models.Params.UI;

namespace BenDing.Service.Interfaces
{
    public interface IWebServiceBasicService
    {/// <summary>
     /// 获取验证码
     /// </summary>
     /// <param name="tradeCode"></param>
     /// <param name="inputParameter"></param>
     /// <returns></returns>
        UserInfoDto GetVerificationCode(string tradeCode, string inputParameter);
        /// <summary>
        /// 获取住院病人      GetCatalog(UserInfoDto user, CatalogParam param)
        /// </summary>
        /// <param name="infoParam"></param>
        /// <returns></returns>
        InpatientInfoDto GetInpatientInfo(GetInpatientInfoParam param);
        /// <summary>
        /// 获取机构
        /// </summary>
        /// <param name="verCode"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Int32 GetOrg(UserInfoDto userinfo, string name);
        /// <summary>
        /// 获取三大目录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string GetCatalog(UserInfoDto user, CatalogParam param);
        /// <summary>
        /// 删除三大目录
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns></returns>
        string DeleteCatalog(UserInfoDto user, int catalog);
        /// <summary>
        /// 获取门诊病人
        /// </summary>
        /// <param name="user"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseOutpatientInfoDto GetOutpatientPerson(UserInfoDto user, GetOutpatientUiParam param,bool isSave);
        /// <summary>
        /// 获取门诊病人明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<BaseOutpatientDetailDto> GetOutpatientDetailPerson(UserInfoDto user, OutpatientDetailParam param);
        /// <summary>
        /// 获取住院病人明细
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>string businessId
        List<InpatientInfoDetailDto> GetInpatientInfoDetail(UserInfoDto user, InpatientInfoDetailParam param, string businessId);
        /// <summary>
        /// 医保信息保存
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        void MedicalInsuranceSave(UserInfoDto user, MedicalInsuranceParam param);
        /// <summary>
        /// 删除医保保存信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        void DeleteMedicalInsurance(UserInfoDto user, DeleteMedicalInsuranceParam param);
        /// <summary>
        ///  获取HIS系统中科室、医师、病区、床位的基本信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        List<InformationDto> SaveInformation(UserInfoDto user, InformationParam param);
        /// <summary>
        /// 获取ICD10
        /// </summary>
        /// <param name="user"></param>
        /// <param name="CatalogParam"></param>
        /// <returns></returns>
        string GetICD10(UserInfoDto user, CatalogParam param);
        void GetXmlData(XmlData param);
        void SaveXmlData(SaveXmlData param);

        UserInfoDto GetUserBaseInfo(string param);

        /// <summary>
        /// 三大目录对码信息回写至基层系统6
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int ThreeCataloguePairCodeUpload(UserInfoDto user);
        /// <summary>
        /// 住院医保查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        QueryMedicalInsuranceDetailInfoDto QueryMedicalInsuranceDetail(
            QueryMedicalInsuranceUiParam param);
    }
}
