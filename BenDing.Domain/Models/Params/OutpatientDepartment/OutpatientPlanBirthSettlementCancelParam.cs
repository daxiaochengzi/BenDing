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
    public class OutpatientPlanBirthSettlementCancelParam
    {
        ///<summary>
        /// 结算单据号
        /// </summary>
        [XmlElementAttribute("PI_DJH", IsNullable = false)]
        [Display(Name = "结算单据号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string SettlementNo { get; set; }
        ///<summary>
        /// 取消备注
        /// </summary>
        [XmlElementAttribute("PI_AAE013", IsNullable = false)]
        [Display(Name = "取消备注")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string CancelRemarks { get; set; }
    }
}
