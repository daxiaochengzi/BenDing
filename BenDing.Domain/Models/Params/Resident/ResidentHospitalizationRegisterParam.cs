using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Resident
{
    [XmlRootAttribute("ROW", IsNullable = false)]
    public class ResidentHospitalizationRegisterParam
    {
        ///<summary>
        /// 身份标识
        /// </summary>
        [XmlElementAttribute("PI_SFBZ", IsNullable = false)]
        public string IdentityMark { get; set; }
        /// <summary>
        /// 传入标志
        /// </summary>
        [XmlElementAttribute("PI_CRBZ", IsNullable = false)]
        public string AfferentSign { get; set; }
        /// <summary>
        /// 医疗类别11：普通入院23: 市内转院住院14: 大病门诊15: 大病住院22: 急诊入院71：顺产72：剖宫产41：病理剖宫产
        /// </summary>
        [XmlElementAttribute("PI_YLLB", IsNullable = false)]
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
        /// 入院日期  (格式为YYYYMMDD)
        /// </summary>
        [XmlElementAttribute("PI_RYRQ", IsNullable = false)]
        public string AdmissionDate { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        [XmlElementAttribute("PI_ICD10", IsNullable = false)]
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
        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 住院科室 
        /// </summary>
        [XmlElementAttribute("PI_ZYBQ", IsNullable = false)]
        public string InpatientDepartment { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
         [XmlElementAttribute("PI_CWH", IsNullable = false)]
        public string BedNumber { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>
        [XmlElementAttribute("PI_YYZYH", IsNullable = false)]
        public string HospitalizationNo { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [XmlElementAttribute("PI_JBR", IsNullable = false)]
        public string Operators { get; set; }

    }
}
