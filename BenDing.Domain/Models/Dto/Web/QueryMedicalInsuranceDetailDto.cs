using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Dto.Web
{
   public class QueryMedicalInsuranceDetailDto
    {
        public Guid? Id { get; set; }
        ///<summary>
        /// 身份标识
        /// </summary>
        [XmlElement("PI_CRBZ", IsNullable = false)]
        [Display(Name = "身份标识")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdentityMark { get; set; }
        /// <summary>
        ///身份证号或者个人编号
        /// </summary>
        [XmlElement("PI_SFBZ", IsNullable = false)]
        [Display(Name = "身份证号或者个人编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AfferentSign { get; set; }
        /// <summary>
        /// 医疗类别11：普通入院23: 市内转院住院14: 大病门诊15: 大病住院22: 急诊入院71：顺产72：剖宫产41：病理剖宫产
        /// </summary>
        [XmlElement("PI_YLLB", IsNullable = false)]
        [Display(Name = " 医疗类别")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalCategory { get; set; }
        /// <summary>
        /// 胎儿数
        /// </summary>
        [XmlElement("PI_TES", IsNullable = false)]
        public string FetusNumber { get; set; }
        /// <summary>
        /// 户口性质
        /// </summary>
        [XmlElement("PI_HKXZ", IsNullable = false)]
        public string HouseholdNature { get; set; }
        /// <summary>
        /// 入院日期(格式为YYYYMMDD)
        /// </summary>

        [XmlElement("PI_RYRQ", IsNullable = false)]
        [Display(Name = "入院日期(格式为YYYYMMDD)")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AdmissionDate { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        [XmlElement("PI_ICD10", IsNullable = false)]
        [Display(Name = "入院主要诊断")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
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
        /// <summary>
        /// 入院诊断疾病名称
        /// </summary>
        [XmlElement("PI_RYZD", IsNullable = false)]
        [Display(Name = "入院诊断疾病名称")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 住院科室编号 
        /// </summary>
        [XmlElement("PI_ZYBQ", IsNullable = false)]
        [Display(Name = "住院科室编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string InpatientDepartmentCode { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
        [XmlElement("PI_CWH", IsNullable = false)]
        public string BedNumber { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>
        [XmlElement("PI_YYZYH", IsNullable = false)]
        [Display(Name = "医院住院号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string HospitalizationNo { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [XmlElement("PI_JBR", IsNullable = false)]
        //[Display(Name = "经办人")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]
        public string Operators { get; set; }
    }
}
