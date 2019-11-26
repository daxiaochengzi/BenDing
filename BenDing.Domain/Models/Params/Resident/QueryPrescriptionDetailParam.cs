using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
/// 
/// </summary>
    [XmlRootAttribute("ROW", IsNullable = false)]
    public class QueryPrescriptionDetailParam
    {/// <summary>
    /// 医保住院号
    /// </summary>
        [XmlElementAttribute("PI_ZYH", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
    }
}
