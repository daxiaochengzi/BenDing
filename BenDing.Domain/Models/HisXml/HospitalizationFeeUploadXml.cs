using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Params.Resident;

namespace BenDing.Domain.Models.HisXml
{
    [XmlRoot("output", IsNullable = false)]
    public class HospitalizationFeeUploadXml
    {/// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElement("akc190", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        ///  
        /// </summary>
        [XmlArrayAttribute("sqldata")]
        [XmlArrayItem("row")]
        public List<HospitalizationFeeUploadRowXml> RowDataList { get; set; }
    }
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class HospitalizationFeeUploadRowXml
    {/// <summary>
    /// 流水号
    /// </summary>
        [XmlElementAttribute("ykc120", IsNullable = false)]
        public string SerialNumber { get; set; }
   
    }
}
