using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 
/// </summary>
   public class ResidentHospitalizationRegisterUiParam:UiBaseDataParam
    {
        ///<summary>
        /// 传入标志 (1为公民身份号码 2为个人编号)
        /// </summary>
        [XmlElement("PI_CRBZ", IsNullable = false)]
        [Display(Name = "身份标识")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string   AfferentSign { get; set; }
        /// <summary>
        ///身份标识 (身份证号或者个人编号)
        /// </summary>
        [XmlElement("PI_SFBZ", IsNullable = false)]
        [Display(Name = "身份标识")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdentityMark { get; set; }
        /// <summary>
        /// 医疗类别11：普通入院23: 市内转院住院14: 大病门诊15: 大病住院22: 急诊入院71：顺产72
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
        /// 医保类型编码
        /// </summary>
        [Display(Name = "医保类型编码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnoreAttribute]
        public  string InsuranceType { get; set; }
        /// <summary>
        /// 结算xml
        /// </summary>
        [XmlIgnore]
        public string SettlementJson { get; set; }
        /// <summary>
        /// 诊断
        /// </summary>
        [XmlIgnore]
        public List<InpatientDiagnosisDto> DiagnosisList { get; set; } = null;
    }
}
