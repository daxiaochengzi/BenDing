﻿using BenDing.Domain.Models.Dto.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 
/// </summary>
  public  class HospitalizationModifyUiParam: UiInIDataParam
    {/// <summary>
     /// 医保住院号
     /// </summary>
        [XmlElement("PI_ZHY", IsNullable = false)]
        [Display(Name = "医保住院号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalInsuranceHospitalizationNo { get; set; }

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

        public string AdmissionDate { get; set; }
        ///// <summary>
        ///// 入院主要诊断疾病ICD-10编码
        ///// </summary>
        //[XmlElement("PI_ICD10", IsNullable = false)]

        //public string AdmissionMainDiagnosisIcd10 { get; set; }
        ///// <summary>
        ///// 入院诊断疾病ICD-10编码
        ///// </summary>
        //[XmlElement("PI_ICD10_2", IsNullable = false)]
        //public string DiagnosisIcd10Two { get; set; }
        ///// <summary>
        ///// 入院诊断疾病ICD-10编码three
        ///// </summary>
        //[XmlElement("PI_ICD10_3", IsNullable = false)]
        //public string DiagnosisIcd10Three { get; set; }
        ///// <summary>
        ///// 入院诊断疾病名称
        ///// </summary>
        //[XmlElement("PI_RYZD", IsNullable = false)]

        //public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 住院科室编号 
        /// </summary>
        [XmlElement("PI_ZYBQ", IsNullable = false)]

        public string InpatientDepartmentCode { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
        [XmlElement("PI_CWH", IsNullable = false)]
        public string BedNumber { get; set; }
        /// <summary>
        /// 诊断
        /// </summary>
        [XmlIgnoreAttribute]
        public List<InpatientDiagnosisDto> DiagnosisList { get; set; } = null;

    }
}