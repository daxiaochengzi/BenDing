using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Resident
{
    [XmlRootAttribute("ROW", IsNullable = false)]
    public class DeletePrescriptionUploadParam
    {
        /// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElement("PI_ZYH", IsNullable = false)]
       
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        [XmlElement("PI_PCH", IsNullable = false)]
        public string BatchNumber { get; set; }
        
    }
}
