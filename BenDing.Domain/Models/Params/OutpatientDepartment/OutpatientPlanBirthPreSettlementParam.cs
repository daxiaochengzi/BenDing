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
    public class OutpatientPlanBirthPreSettlementParam
    {
        ///<summary>
        /// 身份标识 身份证号或者个人编号
        /// </summary>
        [XmlElementAttribute("PI_SFBZ", IsNullable = false)]
        [Display(Name = "身份标识")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdentityMark { get; set; }
        
        /// <summary>
        ///身份证号或者个人编号
        /// </summary>
        [XmlElementAttribute("PI_CRBZ", IsNullable = false)]
        [Display(Name = "传入标志")]
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
        [XmlArrayItem("ROW")]
        public List<PlanBirthSettlementRow> RowDataList { get; set; }

    }
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class PlanBirthSettlementRow
    {/// <summary>
        /// 序号
        /// </summary>
        [XmlElementAttribute("BKE019", IsNullable = false)]
        public int ColNum { get; set; }
        /// <summary>
        /// 医保项目编号
        /// </summary>
        [XmlElementAttribute("AKE001", IsNullable = false)]
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [XmlElementAttribute("AKE002", IsNullable = false)]
        public string ProjectName { get; set; }

        /// <summary>
        /// 单价   (12,4)
        /// </summary>
        [XmlElementAttribute("CKE521", IsNullable = false)]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 数量   (12,4)
        /// </summary>
        [XmlElementAttribute("AKC226", IsNullable = false)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// 总金额 ((12,4)每条费用明细的数据校验为传入的金额（四舍五入到两位小数）和传入的单价*传入的数量（四舍五入到两位小数）必须相等，检查不等的会提示报错
        /// </summary>
        [XmlElementAttribute("CKC526", IsNullable = false)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 医院内剂型
        /// </summary>
        [XmlElementAttribute("AKA070", IsNullable = false)]
        public string Formulation { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        [XmlElementAttribute("AKA071", IsNullable = false)]
        public string Dosage { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        [XmlElementAttribute("AKA073", IsNullable = false)]
        public string Usage { get; set; }
        /// <summary>
        /// 医院内规格
        /// </summary>
        [XmlElementAttribute("AKA074", IsNullable = false)]
        public string Specification { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        [XmlElementAttribute("BKC046", IsNullable = false)]
        public string ManufacturerName { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElementAttribute("AAE013", IsNullable = false)]
        public string Remark { get; set; }


    }
}
