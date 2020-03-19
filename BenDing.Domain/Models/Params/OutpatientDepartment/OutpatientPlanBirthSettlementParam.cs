using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{
    [XmlRoot("ROW", IsNullable = false)]
   public class OutpatientPlanBirthSettlementParam
    {
        ///<summary>
        /// 身份标识
        /// </summary>
        [XmlElementAttribute("PI_CRBZ", IsNullable = false)]
        [Display(Name = "身份标识")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdentityMark { get; set; }
        /// <summary>
        ///身份证号或者个人编号
        /// </summary>
        [XmlElementAttribute("PI_SFBZ", IsNullable = false)]
        [Display(Name = "身份证号或者个人编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AfferentSign { get; set; }
        /// <summary>
        ///门诊号
        /// </summary>
        [XmlElementAttribute("PI_AKC190", IsNullable = false)]
        [Display(Name = "门诊号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string OutpatientNo { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        [XmlElementAttribute("PI_ICD10", IsNullable = false)]
        //[Display(Name = "入院主要诊断")]
        //[Required(ErrorMessage = "{0}不能为空!!!")]
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 诊断日期(格式为YYYYMMDD)
        /// </summary>

        [XmlElementAttribute("PI_AKE010", IsNullable = false)]
        [Display(Name = "诊断日期(格式为YYYYMMDD)")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string DiagnosisDate { get; set; }
        /// <summary>
        /// 项目数量
        /// </summary>

        [XmlElementAttribute("PI_NUM", IsNullable = false)]
        [Display(Name = "项目数量")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public int ProjectNum { get; set; }
        /// <summary>
        /// 金额合计
        /// </summary>

        [XmlElementAttribute("PI_CKC501", IsNullable = false)]
        [Display(Name = "金额合计")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public Decimal TotalAmount { get; set; }
        /// <summary>
        ///  
        /// </summary>
        [XmlArrayAttribute("PI_FYMX")]
        [XmlArrayItem("row")]
        public List<PlanBirthSettlementRow> RowDataList { get; set; }
    }
}
