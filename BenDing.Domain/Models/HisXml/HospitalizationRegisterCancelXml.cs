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
   public class HospitalizationRegisterCancelXml
    {/// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElementAttribute("akc190", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
    }
}
