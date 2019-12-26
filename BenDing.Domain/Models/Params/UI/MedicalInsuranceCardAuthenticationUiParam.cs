using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.UI
{
   public class MedicalInsuranceCardAuthenticationUiParam:UiBaseDataParam
    { /// <summary>
        /// 身份标志身份证号或个人编号
        /// </summary>
        [Display(Name = "身份证号或个人编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
   
        public string InformationNumber { get; set; }
        /// <summary>
        /// 身份标志 1为公民身份号码 2为个人编号
        /// </summary>
        [Display(Name = "身份标志")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [StringLength(1, ErrorMessage = "身份标志输入过长，不能超过1位")]
      
        public string IdentityMark { get; set; }
    }
}
