using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.DifferentPlaces
{/// <summary>
    /// 取消入院登记参数
    /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public  class CanCelDifferentPlacesHospitalizationRegisterParam
    { /// <summary>
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

        /// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElement("AAZ217", IsNullable = false)]
        [Display(Name = "医保住院号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string MedicalInsuranceHospitalizationNo { get; set; }

        /// <summary>
        ///身份证号
        /// </summary>
        [XmlElement("AAC002", IsNullable = false)]
        [Display(Name = "身份证号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdCardNo { get; set; }
        /// <summary>
        ///姓名
        /// </summary>
        [XmlElement("AAC003", IsNullable = false)]
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string PatientName { get; set; }
        /// <summary>
        ///经办人
        /// </summary>
        [XmlElement("BKC131", IsNullable = false)]
        [Display(Name = "经办人")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string Operator { get; set; }
    }
}
