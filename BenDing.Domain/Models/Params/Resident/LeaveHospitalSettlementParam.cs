using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
/// 出院结算参数
/// </summary>
    [XmlRootAttribute("ROW", IsNullable = false)]
    public class LeaveHospitalSettlementParam
    {/// <summary>
     /// 医保住院号
     /// </summary>
        [XmlElementAttribute("PI_ZYH", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 出院日期yyyymmdd
        /// </summary>
        [XmlElementAttribute("PI_CYRQ", IsNullable = false)]
        public string LeaveHospitalDate { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        [XmlElementAttribute("PI_CZY", IsNullable = false)]
        public string UserId { get; set; }
        /// <summary>
        /// 出院主要诊断疾病ICD-10编码
        /// </summary>
        [XmlElementAttribute("PI_ICD10", IsNullable = false)]
        [Display(Name = "入院主要诊断")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string LeaveHospitalMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 出院诊断疾病ICD-10编码
        /// </summary>
        [XmlElementAttribute("PI_ICD10_2", IsNullable = false)]
        public string LeaveHospitalDiagnosisIcd10Two { get; set; }
        /// <summary>
        /// 出院诊断疾病ICD-10编码three
        /// </summary>
        [XmlElementAttribute("PI_ICD10_3", IsNullable = false)]
        public string LeaveHospitalDiagnosisIcd10Three { get; set; }
        /// <summary>
        /// 出院病人状态(1康复，2转院，3死亡，4其他)
        /// </summary>
        [XmlElementAttribute("PI_CYQK", IsNullable = false)]
       
        public string LeaveHospitalInpatientState { get; set; }
        /// <summary>
        /// 出院诊断疾病名称
        /// </summary>
        [XmlElementAttribute("PI_CYZD", IsNullable = false)]
        [Display(Name = "出院诊断疾病名称")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string LeaveHospitalMainDiagnosis { get; set; }
    }
}
