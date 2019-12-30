using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{
 
    /// <summary>
    /// 住院预结算
    /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public class HospitalizationPresettlementJsonDto
    {/// <summary>
     /// 发生费用金额
     /// </summary>
        [XmlElementAttribute("PO_FYZE", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_FYZE")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 基本统筹支付
        /// </summary>
        [XmlElementAttribute("PO_TCZF", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_TCZF")]
        public decimal BasicOverallPay { get; set; }
        /// <summary>
        /// 补充医疗保险支付金额
        /// </summary>
        [XmlElementAttribute("PO_BCZF", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_BCZF")]
        public decimal SupplementPayAmount { get; set; }
        /// <summary>
        /// 生育补助
        /// </summary>
        [XmlElementAttribute("PO_SYBZ", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_SYBZ")]
        public decimal BirthAallowance { get; set; }
        /// <summary>
        /// 民政救助报销支付金额
        /// </summary>
        [XmlElementAttribute("PO_MZJZ", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_MZJZ")]
        public decimal CivilAssistancePayAmount { get; set; }
        /// <summary>
        ///  民政重大疾病救助报销支付金额
        /// </summary>
        [XmlElementAttribute("PO_MZDBJZ", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_MZDBJZ")]
        public decimal CivilAssistanceSeriousIllnessPayAmount { get; set; }
        /// <summary>
        ///  精准扶贫报销支付金额
        /// </summary>
        [XmlElementAttribute("PO_JZFP", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_JZFP")]
        public decimal AccurateAssistancePayAmount { get; set; }
        /// <summary>
        ///  民政优扶报销支付金额
        /// </summary>
        [XmlElementAttribute("PO_MZYF", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_MZYF")]
        public decimal CivilServicessistancePayAmount { get; set; }
        /// <summary>
        ///  其它支付金额
        /// </summary>
        [XmlElementAttribute("PO_QTZF", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_QTZF")]
        public decimal OtherPaymentAmount { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        [XmlElementAttribute("PO_BZ", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_BZ")]
        public string Remark { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        [XmlElementAttribute("PO_XJZF", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_XJZF")]
        public decimal CashPayment { get; set; }
        /// <summary>
        /// 起付金额
        /// </summary>
        [XmlElementAttribute("PO_QFJE", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_QFJE")]
        public decimal PaidAmount { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        [XmlElementAttribute("PO_DJH", IsNullable = false)]
        [JsonProperty(PropertyName = "PO_DJH")]
        public string DocumentNo { get; set; }
        /// <summary>
        /// 过程返回值(为1时正常，否则不正常)
        /// </summary>
        [JsonProperty(PropertyName = "PO_FHZ")]
        [XmlElementAttribute("PO_FHZ", IsNullable = false)]

        public string ResultState { get; set; }
        /// <summary>
        /// 系统错误信息
        /// </summary>
        [JsonProperty(PropertyName = "PO_MSG")]
        [XmlElementAttribute("PO_MSG", IsNullable = false)]
        public string Msg { get; set; }


    }
}
