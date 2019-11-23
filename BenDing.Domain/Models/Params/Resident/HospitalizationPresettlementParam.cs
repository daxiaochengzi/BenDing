using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
 /// 住院预结算
 /// </summary>
    [XmlRootAttribute("ROW", IsNullable = false)]
    public class HospitalizationPresettlementParam
    {
        /// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElementAttribute("PI_ZYH", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 出院日期yyyymmdd
        /// </summary>
        [XmlElementAttribute("PI_CYRQ", IsNullable = false)]
        public string LeaveHospitalDate { get; set; }
    }
}
