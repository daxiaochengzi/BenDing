using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Resident
{
    public class QueryPrescriptionDetailListDto
    { /// <summary>
      /// 行号
      /// </summary>
        [XmlElement("CO", IsNullable = false)]
        [JsonProperty(PropertyName = "CO")]
        public int ColNum { get; set; }
        /// <summary>
        /// 医院编号
        /// </summary>
        [XmlElement("AKB020", IsNullable = false)]
        [JsonProperty(PropertyName = "AKB020")]
        public string HospitalNumber { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        [XmlElement("AKC190", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC190")]
        public string HospitalizationNo { get; set; }

        /// <summary>
        /// 处方号 len(20)
        /// </summary>
        [XmlElement("AKC220", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC220")]
        public string PrescriptionNum { get; set; }
        /// <summary>
        ///  处方序号
        /// </summary>
        [XmlElement("AKC584", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC584")]
        public int PrescriptionSort { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        [XmlElement("AKC222", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC222")]
        public string ProjectCode { get; set; }
        /// <summary>
        /// 院内目录固定编码 len(20)
        /// </summary>
        [XmlElement("AKC515", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC515")]
        public string FixedEncoding { get; set; }
        /// <summary>
        /// 开处方日期 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        [XmlElement("AKC221", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC221")]
        public DateTime? DirectoryDate { get; set; }
        /// <summary>
        /// 结算处方日期 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        [XmlElement("AAE040", IsNullable = false)]
        [JsonProperty(PropertyName = "AAE040")]
        public DateTime? DirectorySettlementDate { get; set; }
        /// <summary>
        /// 收费类别
        /// </summary>
        [XmlElement("AKA063", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA063")]
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [XmlElement("AKC223", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC223")]
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目级别
        /// </summary>
        [XmlElement("AKA065", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA065")]

        public string ProjectLevel { get; set; }
        /// <summary>
        /// 单价   (12,4)
        /// </summary>
        [XmlElement("AKC225", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC225")]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 数量   (12,4)
        /// </summary>
        [XmlElement("AKC226", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC226")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// 金额  (12,4)每条费用明细的数据校验为传入的金额（四舍五入到两位小数）和传入的单价*传入的数量（四舍五入到两位小数）必须相等，检查不等的会提示报错
        /// </summary>
        [XmlElement("AKC227", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC227")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 居民自付金额 (12,2)
        /// </summary>
        [XmlElement("AKC228", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC228")]
        public decimal ResidentSelfPayProportion { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        [XmlElement("AKA070", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA070")]
        public string Formulation { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        [XmlElement("AKA071", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA071")]
        public decimal Dosage { get; set; }

        /// <summary>
        /// 使用频率
        /// </summary>
        [XmlElement("AKA072", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA072")]
        public string UseFrequency { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        [XmlElement("AKA073", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA073")]
        public string Usage { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [XmlElement("AKA074", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA074")]
        public string Specification { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [XmlElement("AKA067", IsNullable = false)]
        [JsonProperty(PropertyName = "AKA067")]
        public string Unit { get; set; }
        /// <summary>
        /// 执行天数
        /// </summary>
        [XmlElement("AKC229", IsNullable = false)]
        [JsonProperty(PropertyName = "AKC229")]
        public int UseDays { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("AAE013", IsNullable = false)]
        [JsonProperty(PropertyName = "AAE013")]
        public string Remark { get; set; }
    }
}
