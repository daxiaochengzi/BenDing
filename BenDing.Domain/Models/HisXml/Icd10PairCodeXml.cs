using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{
    [XmlRoot("sqldata")]
    public  class Icd10PairCodeXml
    {
        [XmlElement]
        public List<Icd10PairCodeDateXml>  row { get; set; }
    }
    
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Icd10PairCodeDateXml
    {
        /// <summary>
        /// 疾病类型
        /// </summary>
        [XmlElement("icd10id", IsNullable = false)]
        public string DiseaseId { get; set; }
        /// <summary>
        /// 疾病编码
        /// </summary>
        [XmlElement("aka120h", IsNullable = false)]
        public string DiseaseCoding { get; set; }
        /// <summary>
        /// 疾病名称
        /// </summary>
        [XmlElement("aka121h", IsNullable = false)]
        public string DiseaseName { get; set; }
    }
}
