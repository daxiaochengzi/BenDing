using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity.DifferentPlaces
{/// <summary>
/// 异地入院登记回参
/// </summary>
   public class DifferentPlacesHospitalizationRegisterJsonDto
    {  /// <summary>
        /// 个人账户余额
        /// </summary>
        [JsonProperty(PropertyName = "PO_GRZHYE")]
        public Decimal AccountBalance { get; set; }
        /// <summary>
        /// 医保住院号
        /// </summary>
        [JsonProperty(PropertyName = "po_zyh")]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 出生日期 Birthday
        /// </summary>

        [JsonProperty(PropertyName = "po_csrq")]
        public string Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
      
        [JsonProperty(PropertyName = "po_gmsfz")]
        public string IdCardNo { get; set; }
        /// <summary>
        /// 参保身份
        /// </summary>
        [JsonProperty(PropertyName = "po_cbsf")]
        public string InsuredStatus { get; set; }
        /// <summary>
        /// 性别
        /// </summary>

        [JsonProperty(PropertyName = "PO_XB")]
        public string PatientSex { get; set; }

        /// <summary>
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险根据获取的险种类型，调用对应的职工或者居民接口办理入院。
        /// </summary>

        [JsonProperty(PropertyName = "PO_XZLX")]
        public string InsuranceType { get; set; }
        /// <summary>
        /// 特殊人员类别
        /// </summary>
        [JsonProperty(PropertyName = "po_tsrylb")] 
        public string SpecialPersonnelType { get; set; }
        /// <summary>
        /// 参保人所属行政区域
        /// </summary>

        [JsonProperty(PropertyName = "PO_XZQH")]
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>

        [JsonProperty(PropertyName = "PO_XM")]
        public string PatientName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>

        [JsonProperty(PropertyName = "PO_DWMC")]
        public string CompanyName { get; set; }
        /// <summary>
        /// 单位编号
        /// </summary>
        [JsonProperty(PropertyName = "po_dwbh")]
        public string CompanyCode { get; set; }
        /// <summary>
        /// 享受待遇标识
        /// </summary>
        [JsonProperty(PropertyName = "po_xsdybs")]
        public string EnjoySign { get; set; }

        /// <summary>
        /// 未享受待遇原因
        /// </summary>
        [JsonProperty(PropertyName = "po_bxsdyyy")]
        public string NotEnjoyCause { get; set; }
    }
}
