using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.DifferentPlaces
{    /// <summary>
    /// 修改入院登记参数
    /// </summary>
       [XmlRoot("ROW", IsNullable = false)]
        public class ModifyDifferentPlacesHospitalizationParam
        {  /// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElement("AAZ217", IsNullable = false)]
        [Display(Name = "医保住院号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        ///<summary>
        /// 个人编号
        /// </summary>
        [XmlElement("AAC001", IsNullable = false)]
        [Display(Name = "个人编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string PersonalNumber { get; set; }
        /// <summary>
        ///身份证号
        /// </summary>
        [XmlElement("AAC002", IsNullable = false)]
        [Display(Name = "身份证号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdCardNo { get; set; }
        /// <summary>
        ///社保卡号（异地就医时会校验）
        /// </summary>
        [XmlElement("AAZ500", IsNullable = false)]
        [Display(Name = "社保卡号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string InsuranceNo { get; set; }
        /// <summary>
        /// 医疗类别  1医疗,2工伤医疗,3工伤康复
        /// </summary>
        [XmlElement("AKA042", IsNullable = false)]
        [Display(Name = " 医疗类别")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalCategory { get; set; }
        /// <summary>
        /// 参保地统筹编码(即病人所属行政区划代码)
        /// </summary>
        [XmlElement("BAA008", IsNullable = false)]
        [Display(Name = "参保地统筹编码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string OverallCoding { get; set; }
        /// <summary>
        /// 住院科室编号 
        /// </summary>
        [XmlElement("AAZ307", IsNullable = false)]
        [Display(Name = "住院科室编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string InpatientDepartmentCode { get; set; }
        /// <summary>
        /// 住院科室编号 
        /// </summary>
        [XmlElement("BKC018", IsNullable = false)]
        [Display(Name = "住院科室名称")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string InpatientDepartmentName { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        [XmlElement("AKC193", IsNullable = false)]
        [Display(Name = "入院主要诊断疾病ICD-10编码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码
        /// </summary>
        [XmlElement("BKC021", IsNullable = false)]
        public string DiagnosisIcd10Two { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码three
        /// </summary>
        [XmlElement("BKC022", IsNullable = false)]
        public string DiagnosisIcd10Three { get; set; }
        /// <summary>
        /// 入院诊断主要疾病名（由医院自行组织）
        /// </summary>
        [XmlElement("BKC020", IsNullable = false)]
        [Display(Name = "入院诊断主要疾病名")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [StringLength(200, ErrorMessage = "入院诊断主要疾病名输入过长，不能超过200位")]
        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 次要诊断
        /// </summary>
        [XmlArray("BKC033")]
        [XmlArrayItem("ROW")]
        public List<DifferentPlacesOtherDiagnosis> DiagnosisList { get; set; }
        /// <summary>
        /// 病历编号 len(20)
        /// </summary>
        [XmlElement("BKC015", IsNullable = false)]
        public string MedicalRecordNo { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>
        [XmlElement("AKC190", IsNullable = false)]
        [Display(Name = "医院住院号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string HospitalizationNo { get; set; }
        /// <summary>
        /// 入院日期(格式为yyyy-mm-dd hh:mi:ss)
        /// </summary>
        [XmlElement("AAE030", IsNullable = false)]
        [Display(Name = "入院日期")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AdmissionDate { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
        [XmlElement("BKC019", IsNullable = false)]
        [Display(Name = "床位号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string BedNumber { get; set; }
        /// <summary>
        /// 入院诊断医生编码
        /// </summary>
        [XmlElement("AKE022_BM", IsNullable = false)]
        public string HospitalizedDiagnosisDoctorCode { get; set; }
        /// <summary>
        /// 入院诊断医生名称
        /// </summary>
        [XmlElement("AKE022", IsNullable = false)]
        public string HospitalizedDiagnosisDoctorName { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [XmlElement("BKC017", IsNullable = false)]
        [Display(Name = "经办人")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string Operators { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [XmlElement("AAE004", IsNullable = false)]
        public string Contacts { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        [XmlElement("AAE005", IsNullable = false)]
        public string ContactsPhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("AAE013", IsNullable = false)]
        public  string  Remarks { get; set; }
    }
}
