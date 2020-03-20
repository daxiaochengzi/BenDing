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
    public  class OutpatientPlanBirthSettlementQueryParam
    {///<summary>
     /// 身份标识 (1为公民身份号码 2为个人编号)
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
        ///<summary>
        /// 结算单据号
        /// </summary>
        [XmlElementAttribute("PI_DJH", IsNullable = false)]
        [Display(Name = "结算单据号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string SettlementNo { get; set; }
    }
}
