using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.DifferentPlaces
{/// <summary>
/// 出院办理
/// </summary>
   public class LeaveHospitalHandleParam
    {
        /// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElement("AAZ217", IsNullable = false)]
        [Display(Name = "医保住院号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 参保地统筹编码(即病人所属行政区划代码)
        /// </summary>
        [XmlElement("BAA008", IsNullable = false)]
        [Display(Name = "参保地统筹编码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string OverallCoding { get; set; }
        ///<summary>
        /// 个人编号
        /// </summary>
        [XmlElement("AAC001", IsNullable = false)]
        [Display(Name = "个人编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string PersonalNumber { get; set; }

        ///<summary>
        /// 出院病历号 len(20)
        /// </summary>
        [XmlElement("BKC015", IsNullable = false)]
       
   
        public string LeaveHospitalMedicalRecordNo { get; set; }

        ///<summary>
        /// 出院住院号
        /// </summary>
        [XmlElement("AKC190", IsNullable = false)]
        [Display(Name = "出院住院号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string LeaveHospitalNo{ get; set; }
        ///<summary>
        /// 出院原因
        /// </summary>
        [XmlElement("BKC031", IsNullable = false)]
       
        public string LeaveHospitalReason { get; set; }
        ///<summary>
        /// 出院时间
        /// </summary>
        [XmlElement("AKC190", IsNullable = false)]
        [Display(Name = "出院时间")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string LeaveHospitalTime { get; set; }

        /// <summary>
        /// 出院科室编号 
        /// </summary>
        [XmlElement("AAZ307", IsNullable = false)]
        [Display(Name = "住院科室编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string LeaveHospitalDepartmentCode { get; set; }
        /// <summary>
        /// 出院科室名称 
        /// </summary>
        [XmlElement("BKC025", IsNullable = false)]
       
        public string LeaveHospitalDepartmentName { get; set; }
        /// <summary>
        /// 出院主要诊断疾病ICD-10编码
        /// </summary>
        [XmlElement("AKC196", IsNullable = false)]
        [Display(Name = "出院主要诊断疾病ICD-10编码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string LeaveHospitalMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 出院诊断疾病ICD-10编码
        /// </summary>
        [XmlElement("BKC028", IsNullable = false)]
        public string LeaveHospitalDiagnosisIcd10Two { get; set; }
        /// <summary>
        /// 出院诊断疾病ICD-10编码three
        /// </summary>
        [XmlElement("BKC029", IsNullable = false)]
        public string LeaveHospitalDiagnosisIcd10Three { get; set; }
        /// <summary>
        /// 出院诊断主要疾病名（由医院自行组织）
        /// </summary>
        [XmlElement("BKC027", IsNullable = false)]
        [Display(Name = "出院诊断主要疾病名")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [StringLength(200, ErrorMessage = "出院诊断主要疾病名名输入过长，不能超过200位")]
        public string LeaveHospitalMainDiagnosis { get; set; }
        /// <summary>
        /// 次要诊断
        /// </summary>
        [XmlArray("BKC033")]
        [XmlArrayItem("ROW")]
        public List<DifferentPlacesLeaveHospitalOtherDiagnosis> DiagnosisList { get; set; }

        /// <summary>
        /// 出院床位号
        /// </summary>
        [XmlElement("BKC025", IsNullable = false)]
    
        public string LeaveHospitalBedNumber { get; set; }
        /// <summary>
        /// 入院诊断医生编码
        /// </summary>
        [XmlElement("AKE021_BM", IsNullable = false)]
        public string LeaveHospitalDiagnosisDoctorCode { get; set; }
        /// <summary>
        /// 入院诊断医生名称
        /// </summary>
        [XmlElement("AKE021", IsNullable = false)]
        public string LeaveHospitalDiagnosisDoctorName { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [XmlElement("BKC024", IsNullable = false)]
        [Display(Name = "经办人")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string Operators { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("AAE013", IsNullable = false)]
        public string Remarks { get; set; }
    }
    /// <summary>
    /// 异地其他诊断
    /// </summary>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class DifferentPlacesLeaveHospitalOtherDiagnosis
    {
        /// <summary>
        /// 诊断名称
        /// </summary>
        [XmlElement("AKC186", IsNullable = false)]
        public string DiagnosisName { get; set; }
        /// <summary>
        /// 诊断编码
        /// </summary>
        [XmlElement("BKC028", IsNullable = false)]
        public string DiagnosisCode { get; set; }
    }
}
