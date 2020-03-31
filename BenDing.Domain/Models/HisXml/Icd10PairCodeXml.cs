using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.HisXml
{
    [XmlRoot("output", IsNullable = false)]
  public  class Icd10PairCodeXml
    {   /// <summary>
        /// 疾病编码
        /// </summary>
        [XmlElement("aka120", IsNullable = false)]
        public string DiseaseCoding { get; set; }
        /// <summary>
        /// 疾病名称
        /// </summary>
        [XmlElement("aka121", IsNullable = false)]
        public string DiseaseName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [XmlElement("aka020", IsNullable = false)]
        public string MnemonicCode { get; set; }
        /// <summary>
        /// 疾病类型
        /// </summary>
        [XmlElement("aka122", IsNullable = false)]
        public string DiseaseType { get; set; }
    }
}
