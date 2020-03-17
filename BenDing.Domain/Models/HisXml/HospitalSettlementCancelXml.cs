using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{
    [XmlRoot("output", IsNullable = false)]
    public class HospitalSettlementCancelXml
    {
  
        /// <summary>
        /// 结算单号
        /// </summary>
        [XmlElementAttribute("aaz216", IsNullable = false)]
        public string SettlementNo { get; set; }
       
    }
}
