using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Resident
{
    [XmlRootAttribute("ROWDATA", IsNullable = false)]
    public class ResidentProjectDownloadDto
    {
        
        [XmlElementAttribute("ROW")]
        [JsonProperty(PropertyName = "Row")]
        public List<ResidentProjectDownloadRowDataRowDto> Row { get; set; }
    }
    
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    
    public class ResidentProjectDownloadRowDataRowDto 
    {/// <summary>
     /// 收费项目编码
     /// </summary>
        [XmlElementAttribute("AKE001", IsNullable = false)]
        [JsonProperty(PropertyName = "AKE001")]

        public string ProjectCode { get; set; }
        /// <summary>
        /// 收费项目名称
        /// </summary>
        [XmlElementAttribute("AKE002", IsNullable = false)]
        [JsonProperty(PropertyName = "AKE002")]
        public string ProjectName { get; set; }
        /// <summary>
        /// 分类代码
        /// </summary>
        [XmlElementAttribute("AKA063", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA063")]
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 收费项目等级
        /// </summary>
        [XmlElementAttribute("AKA065", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA065")]
        public string ProjectLevel { get; set; }
        /// <summary>
        /// 职工医保自付比例
        /// </summary>
        [XmlElementAttribute("AKA069", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA069")]
        public string WorkersSelfPayProportion { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
       
        [XmlElementAttribute("AKA067", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA067")]
        public string Unit { get; set; }
        /// <summary>
        /// 拼音助记码
        /// </summary>
        [XmlElementAttribute("AKA020", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA020")]
        public string MnemonicCode { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        [XmlElementAttribute("AKA070", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA070")]
        public string Formulation { get; set; }
        /// <summary>
        /// 居民医保自付比例
        /// </summary>
        [XmlElementAttribute("CKE899", IsNullable = false)]
        [JsonProperty(PropertyName = "CKE899")]
        public string ResidentSelfPayProportion { get; set; }
        
        /// <summary>
        /// 限制用药标志
        /// </summary>
        [XmlElementAttribute("AKA036", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA036")]
        public string RestrictionSign { get; set; }
        /// <summary>
        /// 零档限价（二级乙等以下）
        /// </summary>
        [XmlElementAttribute("CKA599", IsNullable = false)]
        [JsonProperty(PropertyName = "CKA599")]
        public string ZeroBlock { get; set; }
        /// <summary>
        /// 一档限价（二级乙等）
        /// </summary>
        [XmlElementAttribute("CKA578", IsNullable = false)]
        [JsonProperty(PropertyName = "CKA578")]
        public string OneBlock { get; set; }
        /// <summary>
        /// 二档限价（二级甲等）
        /// </summary>
        [XmlElementAttribute("CKA579", IsNullable = false)]
        [JsonProperty(PropertyName = "CKA579")]
        public string TwoBlock { get; set; }
        /// <summary>
        /// 三档限价（三级乙等）
        /// </summary>
        [XmlElementAttribute("CKA580", IsNullable = false)]
        [JsonProperty(PropertyName = "CKA580")]
        public string ThreeBlock { get; set; }

        /// <summary>
        /// 四档限价（三级甲等）
        /// </summary>
        [XmlElementAttribute("CKA560", IsNullable = false)]
        [JsonProperty(PropertyName = "CKA560")]
        public string FourBlock { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        [XmlElementAttribute("AAE100", IsNullable = false)]
        [JsonProperty(PropertyName = "AAE100")]
        public string EffectiveSign { get; set; }
        /// <summary>
        /// 居民普通门诊报销标志
        /// </summary>
        [XmlElementAttribute("CKE889", IsNullable = false)]
        [JsonProperty(PropertyName = "CKE889")]
        public string ResidentOutpatientSign { get; set; }
        /// <summary>
        /// 居民普通门诊报销限价
        /// </summary>
        [XmlElementAttribute("CKA601", IsNullable = false)]
        [JsonProperty(PropertyName = "CKA601")]
        public string ResidentOutpatientBlock { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        [XmlElementAttribute("AKA098", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA098")]
        public string Manufacturer { get; set; }
        /// <summary>
        /// 药品准字号
        /// </summary>
        [XmlElementAttribute("CKA603", IsNullable = false)]
        [JsonProperty(PropertyName = "CKA603")]
        public string QuasiFontSize { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [XmlElementAttribute("AKA074", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA074")]
        public string Specification { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElementAttribute("AAE013", IsNullable = false)]
        [JsonProperty(PropertyName = "AAE013")]
        public string Remark { get; set; }
        /// <summary>
        /// 新码标志
        /// </summary>
        [XmlElementAttribute("CKE897", IsNullable = false)]
        [JsonProperty(PropertyName = "CKE897")]
        public string NewCodeMark { get; set; }
        /// <summary>
        /// 最近一次变更日期
        /// </summary>
        [XmlElementAttribute("AAE036", IsNullable = false)]
        [JsonProperty(PropertyName = "AAE036")]
        public string NewUpdateTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [XmlElementAttribute("AAE030", IsNullable = false)]
        [JsonProperty(PropertyName = "AAE030")]
        public string StartTime { get; set; }
        /// <summary>
        /// 终止时间
        /// </summary>
        [XmlElementAttribute("AAE031", IsNullable = false)]
        [JsonProperty(PropertyName = "AAE031")]
        public string EndTime { get; set; }

        /// <summary>
        /// 限制支付范围
        /// </summary>
        [XmlElementAttribute("CKE599", IsNullable = false)]
        [JsonProperty(PropertyName = "CKE599")]
        public string LimitPaymentScope { get; set; }
    }
}
