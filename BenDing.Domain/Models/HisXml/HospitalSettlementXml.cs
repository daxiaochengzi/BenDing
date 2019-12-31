using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{
    [XmlRoot("output", IsNullable = false)]
    public class HospitalSettlementXml
    {
        /// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElementAttribute("akc190", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }

        /// <summary>
        /// 结算单号
        /// </summary>
        [XmlElementAttribute("aaz216", IsNullable = false)]
        public string SettlementNo { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        [XmlElementAttribute("aac003", IsNullable = false)]
        public string PatientName { get; set; }
        /// <summary>
        /// 医保卡号
        /// </summary>
        [XmlElementAttribute("yac005", IsNullable = false)]
        public string InsuranceNo { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        [XmlElementAttribute("ykc107", IsNullable = false)]
       
        public  decimal AccountBalance { get; set; }
        /// <summary>
        /// 总费用
        /// </summary>
        [XmlElementAttribute("akc264", IsNullable = false)]
        public decimal AllAmount { get; set; }
      
        /// <summary>
        /// 起付金额
        /// </summary>
        [XmlElementAttribute("yka115", IsNullable = false)]
       
        public decimal PaidAmount { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        [XmlElementAttribute("yka133", IsNullable = false)]

        public decimal CashPayment { get; set; }
        /// <summary>
        /// 个人账户支付
        /// </summary>
        [XmlElementAttribute("ake034", IsNullable = false)]
        public decimal AccountAmountPay { get; set; }
    } 
}
