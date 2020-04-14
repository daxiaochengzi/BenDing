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
{
   public class OutpatientPlanBirthPreSettlementUiParam: UiBaseDataParam
    {
        ///<summary>
        /// 身份标识
        /// </summary>
        [Display(Name = "身份标识")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdentityMark { get; set; }
        /// <summary>
        ///身份证号或者个人编号
        /// </summary>
        [Display(Name = "身份证号或者个人编号")]  
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string AfferentSign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SettlementJson { get; set; }

}
}
