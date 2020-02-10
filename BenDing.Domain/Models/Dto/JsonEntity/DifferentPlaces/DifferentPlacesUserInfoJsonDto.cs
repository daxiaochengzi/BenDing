using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity.DifferentPlaces
{/// <summary>
/// 异地病人信息
/// </summary>
   public class DifferentPlacesUserInfoJsonDto
    {/// <summary>
     /// 个人编码
     /// </summary>

        [JsonProperty(PropertyName = "PO_GRSHBZH")]
        public string PersonalCoding { get; set; }

        /// <summary>
        /// 行政区域
        /// </summary>

        [JsonProperty(PropertyName = "PO_XZQH")]
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>

        [JsonProperty(PropertyName = "PO_XM")]
        public string PatientName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>

        [JsonProperty(PropertyName = "PO_XB")]
        public string PatientSex { get; set; }
        /// <summary>
        /// 出生日期 Birthday
        /// </summary>

        [JsonProperty(PropertyName = "PO_CSNY")]
        public string Birthday { get; set; }
        /// <summary>
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险根据获取的险种类型，调用对应的职工或者居民接口办理入院。
        /// </summary>

        [JsonProperty(PropertyName = "PO_XZLX")]
        public string InsuranceType { get; set; }
        /// <summary>
        /// 参保状态
        /// </summary>

        [JsonProperty(PropertyName = "PO_CBZT")]
        public string InsuredState { get; set; }
        /// <summary>
        /// 医院住院报销状态
        /// </summary>

        [JsonProperty(PropertyName = "PO_YBTSZT")]
        public string ReimbursementStatus { get; set; }
        /// <summary>
        /// 报销状态说明
        /// </summary>

        [JsonProperty(PropertyName = "P0_YBBXZTSM")]
        public string ReimbursementStatusExplain { get; set; }
        /// <summary>
        /// 人员分类
        /// </summary>

        [JsonProperty(PropertyName = "PO_RYFL")]
        public string PersonnelClassification { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>

        [JsonProperty(PropertyName = "PO_LXDZ")]
        public string ContactAddress { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>

        [JsonProperty(PropertyName = "PO_LXDH")]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>

        [JsonProperty(PropertyName = "PO_SFZH")]
        public string IdCardNo { get; set; }
        /// <summary>
        /// 实足年龄
        /// </summary>

        [JsonProperty(PropertyName = "PO_SZNL")]
        public string Age { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>

        [JsonProperty(PropertyName = "PO_DWMC")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 统筹支付余额
        /// </summary>
        [JsonProperty(PropertyName = "PO_TCZFYE")]
        public decimal OverallPaymentBalance { get; set; }
        /// <summary>
        /// 职工类别
        /// </summary>
        [JsonProperty(PropertyName = "PO_ZGLB")]
        public string WorkersType { get; set; }
        /// <summary>
        /// 特殊人员身份 
        /// </summary>
        [JsonProperty(PropertyName = "PO_TSRYSF")]
        public  string SpecialPersonnel { get; set; }
        /// <summary>
        /// 个人账户余额
        /// </summary>
        [JsonProperty(PropertyName = "PO_GRZHYE")]
        public Decimal AccountBalance { get; set; }
         
    }
}
