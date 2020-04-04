using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{/// <summary>
 /// 入院登记
 /// </summary>
    [XmlRoot("output", IsNullable = false)]
    public class HospitalizationRegisterXml
    {/// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElementAttribute("akc190", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }

        /// <summary>
        /// 社保卡号
        /// </summary>
        [XmlElementAttribute("yac005", IsNullable = false)]
        public string InsuranceNo { get; set; }

        /// <summary>
        /// 医保类型  职工:1 ;342:居民 
        /// </summary>
        [XmlElementAttribute("aae140", IsNullable = false)]
        public string MedicalInsuranceType { get; set; }

    }
}
