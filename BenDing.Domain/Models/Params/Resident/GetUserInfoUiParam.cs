using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Resident
{
  public  class GetUserInfoUiParam
    { /// <summary>
        /// 身份标志身份证号或个人编号
        /// </summary>
        [Display(Name = "身份证号或个人编号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]

        public string IdentityMark { get; set; }
        /// <summary>
        /// 传入标志 1为公民身份号码 2为个人编号
        /// </summary>
        [Display(Name = "传入标志")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
       
        public string AfferentSign { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string UserId { get; set; }
    }
}
