using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{
   public class ResidentUserInfoJsonDto: IniDto
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
        /// 报销状态
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
        /// 社区名称
        /// </summary>

        [JsonProperty(PropertyName = "PO_SQMC")]
        public string CommunityName { get; set; }
        /// <summary>
        /// 居民医保账户余额ResidentInsuranceBalance
        /// </summary>

        [JsonProperty(PropertyName = "PO_JBZHYE")]
        public decimal ResidentInsuranceBalance { get; set; }
        /// <summary>
        /// 职工医保账户余额 
        /// </summary>

        [JsonProperty(PropertyName = "PO_ZGZHYE")]
        public decimal WorkersInsuranceBalance { get; set; }
        /// <summary>
        /// 门特余额
        /// </summary>

        [JsonProperty(PropertyName = "PO_MTYE")]
        public decimal MentorBalance { get; set; }
        /// <summary>
        /// 建卡贫困户标志
        /// </summary>

        [JsonProperty(PropertyName = "PO_PKHBZ")]
        public string PoorMark { get; set; }
        /// <summary>
        /// 重点救助对象类别
        /// </summary>

        [JsonProperty(PropertyName = "PO_MZJZLB")]
        public string RescueType { get; set; }
        /// <summary>
        /// 重点优抚对象类别
        /// </summary>
        [JsonProperty(PropertyName = "PO_MZYFLB")]
        public string PreferentialTreatmentType { get; set; }
        /// <summary>
        /// 民政特殊人员认定地
        /// </summary>
        [JsonProperty(PropertyName = "PO_MZRDD")]
        public string SpecialPeopleCognizancePlace { get; set; }
        /// <summary>
        /// 统筹支付余额
        /// </summary>
        [JsonProperty(PropertyName = "PO_TCZFYE")]
        public decimal OverallPaymentBalance { get; set; }
    }
}
