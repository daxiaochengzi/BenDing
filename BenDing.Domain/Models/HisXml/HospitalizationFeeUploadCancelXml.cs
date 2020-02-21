using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{/// <summary>
/// 基层处方回退入参
/// </summary>
    [XmlRoot("output", IsNullable = false)]
    public class HospitalizationFeeUploadCancelXml
    {/// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElement("akc190", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }

        /// <summary>
        /// 撤销标记(1撤销所有 0根据返回的ykc120撤销)
        /// </summary>
        [XmlElement("revokeall", IsNullable = false)]
        public int RevokeAll { get; set; } 
        /// <summary>
        ///  
        /// </summary>
        [XmlArrayAttribute("sqldata")]
        [XmlArrayItem("row")]
        public List<HospitalizationFeeUploadRowXml> RowDataList { get; set; }
    }
}
