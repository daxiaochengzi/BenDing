using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{/// <summary>
    /// 读卡
    /// </summary>
    [XmlRoot("output", IsNullable = false)]
    public class ReadCardXml
    {/// <summary>
     /// 身份证号码
     /// </summary>
        [XmlElementAttribute("aac002", IsNullable = false)]
        public string IdCardNo { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        [XmlElementAttribute("aac003", IsNullable = false)]
        public  string PatientName { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [XmlElementAttribute("yke112", IsNullable = false)]
        public string Age { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        [XmlElementAttribute("aae140", IsNullable = false)]
        public string InsuranceType { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        [XmlElementAttribute("aae240", IsNullable = false)]
        public  decimal AccountBalance { get; set; }
}
}
