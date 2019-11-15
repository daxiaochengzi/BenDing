using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Dto.Resident
{
    [XmlRootAttribute("ROWDATA", IsNullable = false)]
    public class ResidentProjectDownloadDto
    {
        
        [XmlElementAttribute("ROW")]
        public List<ResidentProjectDownloadRowDataRowDto> Row { get; set; }
    }
    
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    
    public class ResidentProjectDownloadRowDataRowDto 
    {/// <summary>
     /// 收费项目编码
     /// </summary>
        [XmlElementAttribute("AKE001", IsNullable = false)]

        public string ProjectCode { get; set; }
        /// <summary>
        /// 收费项目名称
        /// </summary>
        [XmlElementAttribute("AKE002", IsNullable = false)]
        public string ProjectName { get; set; }
        /// <summary>
        /// 分类代码
        /// </summary>
        [XmlElementAttribute("AKA063", IsNullable = false)]
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 收费项目等级
        /// </summary>
        [XmlElementAttribute("AKA065", IsNullable = false)]
        public string ProjectLevel { get; set; }
        /// <summary>
        /// 职工医保自付比例
        /// </summary>
        [XmlElementAttribute("AKA069", IsNullable = false)]
        public string WorkersSelfPayProportion { get; set; }
        
        /// <summary>
        /// 单位
        /// </summary>
        [XmlElementAttribute("AKA067", IsNullable = false)]
        public string Unit { get; set; }
        /// <summary>
        /// 拼音助记码
        /// </summary>
        [XmlElementAttribute("AKA020", IsNullable = false)]
        public string MnemonicCode { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        [XmlElementAttribute("AKA070", IsNullable = false)]
        public string Formulation { get; set; }
        /// <summary>
        /// 居民医保自付比例
        /// </summary>
        [XmlElementAttribute("CKE899", IsNullable = false)]
        public decimal ResidentSelfPayProportion { get; set; }
        
        /// <summary>
        /// 限制用药标志
        /// </summary>
        [XmlElementAttribute("AKA036", IsNullable = false)]
        public string RestrictionSign { get; set; }
        /// <summary>
        /// 零档限价（二级乙等以下）
        /// </summary>
        [XmlElementAttribute("CKA599", IsNullable = false)]
        public string ZeroBlock { get; set; }
        /// <summary>
        /// 一档限价（二级乙等）
        /// </summary>
        [XmlElementAttribute("CKA578", IsNullable = false)]
        public string OneBlock { get; set; }
        /// <summary>
        /// 二档限价（二级甲等）
        /// </summary>
        [XmlElementAttribute("CKA579", IsNullable = false)]
        public decimal TwoBlock { get; set; }
        /// <summary>
        /// 三档限价（三级乙等）
        /// </summary>
        [XmlElementAttribute("CKA580", IsNullable = false)]
        public decimal ThreeBlock { get; set; }

        /// <summary>
        /// 四档限价（三级甲等）
        /// </summary>
        [XmlElementAttribute("CKA560", IsNullable = false)]
        public decimal FourBlock { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        [XmlElementAttribute("AAE100", IsNullable = false)]
        public string EffectiveSign { get; set; }
        /// <summary>
        /// 居民普通门诊报销标志
        /// </summary>
        [XmlElementAttribute("CKE889", IsNullable = false)]
        public string ResidentOutpatientSign { get; set; }
        /// <summary>
        /// 居民普通门诊报销限价
        /// </summary>
        [XmlElementAttribute("CKA601", IsNullable = false)]
        public decimal ResidentOutpatientBlock { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
        [XmlElementAttribute("AKA098", IsNullable = false)]
        public string Manufacturer { get; set; }
        /// <summary>
        /// 药品准字号
        /// </summary>
        [XmlElementAttribute("CKA603", IsNullable = false)]
        public string QuasiFontSize { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [XmlElementAttribute("AKA074", IsNullable = false)]
        public string Specification { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElementAttribute("AAE013", IsNullable = false)]
        public string Remark { get; set; }
        /// <summary>
        /// 新码标志
        /// </summary>
        [XmlElementAttribute("CKE897", IsNullable = false)]
        public string NewCodeMark { get; set; }
        /// <summary>
        /// 最近一次变更日期
        /// </summary>
        [XmlElementAttribute("AAE036", IsNullable = false)]
        public string NewUpdateTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [XmlElementAttribute("AAE030", IsNullable = false)]
        public string StartTime { get; set; }
        /// <summary>
        /// 终止时间
        /// </summary>
        [XmlElementAttribute("AAE031", IsNullable = false)]
        public string EndTime { get; set; }

        /// <summary>
        /// 限制支付范围
        /// </summary>
        [XmlElementAttribute("CKE599", IsNullable = false)]
        public string LimitPaymentScope { get; set; }
    }
}
