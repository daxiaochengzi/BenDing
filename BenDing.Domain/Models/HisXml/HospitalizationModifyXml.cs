using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{
  public  class HospitalizationModifyXml
    {/// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElement("akc190", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
    }
}
