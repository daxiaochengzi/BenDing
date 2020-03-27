using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
/// 入院登记参数
/// </summary>
    [XmlRootAttribute("ROW", IsNullable = false)]
    public class ResidentHospitalizationRegisterParam
    {
        ///<summary>
        /// 传入标志 (1为公民身份号码 2为个人编号)
        /// </summary>
        [XmlElementAttribute("PI_CRBZ", IsNullable = false)]
        [Display(Name = "传入标志")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AfferentSign { get; set; }
        /// <summary>
        ///身份标志 （身份证号或者个人编号)
        /// </summary>
        [XmlElementAttribute("PI_SFBZ", IsNullable = false)]
        [Display(Name = "身份标志")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdentityMark { get; set; }
        /// <summary>
        /// 医疗类别11：普通入院23: 市内转院住院14: 大病门诊15: 大病住院22: 急诊入院71：顺产72：剖宫产41：病理剖宫产
        /// </summary>
        [XmlElementAttribute("PI_YLLB", IsNullable = false)]
        [Display(Name = " 医疗类别")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalCategory { get; set; }
        /// <summary>
        /// 胎儿数
        /// </summary>
        [XmlElementAttribute("PI_TES", IsNullable = false)]
        public string FetusNumber { get; set; }
        /// <summary>
        /// 户口性质
        /// </summary>
        [XmlElementAttribute("PI_HKXZ", IsNullable = false)]
        public string HouseholdNature { get; set; }
        /// <summary>
        /// 入院日期(格式为YYYYMMDD)
        /// </summary>
        
        [XmlElementAttribute("PI_RYRQ", IsNullable = false)]
        [Display(Name = "入院日期(格式为YYYYMMDD)")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AdmissionDate { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        [XmlElementAttribute("PI_ICD10", IsNullable = false)]
        //[Display(Name = "入院主要诊断")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码
        /// </summary>
        [XmlElementAttribute("PI_ICD10_2", IsNullable = false)]
        public string DiagnosisIcd10Two { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码three
        /// </summary>
        [XmlElementAttribute("PI_ICD10_3", IsNullable = false)]
        public string DiagnosisIcd10Three { get; set; }
        /// <summary>
        /// 入院诊断疾病名称
        /// </summary>
        [XmlElementAttribute("PI_RYZD", IsNullable = false)]
        [Display(Name = "入院诊断疾病名称")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 住院科室
        /// </summary>
        [XmlElementAttribute("PI_ZYBQ", IsNullable = false)]
        [Display(Name = "住院科室")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string InpatientDepartmentCode { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
        [XmlElementAttribute("PI_CWH", IsNullable = false)]
        public string BedNumber { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>
        [XmlElementAttribute("PI_YYZYH", IsNullable = false)]
        [Display(Name = "医院住院号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string HospitalizationNo { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [XmlElementAttribute("PI_JBR", IsNullable = false)]
        //[Display(Name = "经办人")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]
        public string Operators { get; set; }
        /// <summary>
        /// 医保类型编码(310 职工,342 居民)
        /// </summary>
        [Display(Name = "医保类型编码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnoreAttribute]
        public string InsuranceType { get; set; }

       



    }
}
