using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Workers
{
    [XmlRootAttribute("ROW", IsNullable = false)]
    public class WorkerBirthPreSettlementParam
    { /// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElementAttribute("PI_ZYH", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 71：顺产: 72: 剖宫产
        /// </summary>
        [XmlElement("PI_YLLB", IsNullable = false)]
        [Display(Name = " 医疗类别")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalCategory { get; set; }
        
        /// <summary>
        /// 胎儿数
        /// </summary>
        [XmlElement("PI_TES", IsNullable = false)]
        public int FetusNumber { get; set; }
        /// <summary>
        /// 出院日期yyyymmdd
        /// </summary>
        [XmlElementAttribute("PI_CYRQ", IsNullable = false)]
        public string LeaveHospitalDate { get; set; }
     
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        [XmlElement("PI_ICD10", IsNullable = false)]
        //[Display(Name = "入院主要诊断")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码
        /// </summary>
        [XmlElement("PI_ICD10_2", IsNullable = false)]
        public string DiagnosisIcd10Two { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码three
        /// </summary>
        [XmlElement("PI_ICD10_3", IsNullable = false)]
        public string DiagnosisIcd10Three { get; set; }
        
    }
}
