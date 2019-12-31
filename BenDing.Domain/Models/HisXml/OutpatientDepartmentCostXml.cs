using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{
   public class OutpatientDepartmentCostXml
    {
        /// <summary>
        /// 医保门诊号
        /// </summary>
        [XmlElement("akc190", IsNullable = false)]
        public string MedicalInsuranceOutpatientNo { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        [XmlElement("aac001", IsNullable = false)]
        public string PersonalCoding { get; set; }
        /// <summary>
        /// 医保卡号
        /// </summary>
        [XmlElement("yac005", IsNullable = false)]
        public string InsuranceNo { get; set; }

        /// <summary>
        /// 结算单号
        /// </summary>
        [XmlElement("aaz216", IsNullable = false)]
        public string SettlementNo { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        [XmlElement("aac003", IsNullable = false)]
        public string PatientName { get; set; }
      
        /// <summary>
        /// 账户余额
        /// </summary>
        [XmlElement("ykc107", IsNullable = false)]

        public decimal AccountBalance { get; set; }
        /// <summary>
        /// 总费用
        /// </summary>
        [XmlElement("akc264", IsNullable = false)]
        public decimal AllAmount { get; set; }

        /// <summary>
        /// 现金支付
        /// </summary>
        [XmlElement("yka133", IsNullable = false)]

        public decimal CashPayment { get; set; }
        /// <summary>
        /// 个人账户支付
        /// </summary>
        [XmlElement("ake034", IsNullable = false)]
        public decimal AccountAmountPay { get; set; }

        /// <summary>
        /// 医保类型 10:居民  职工:310
        /// </summary>
        [XmlElementAttribute("aac066", IsNullable = false)]
        public string MedicalInsuranceType { get; set; }
    }
}
