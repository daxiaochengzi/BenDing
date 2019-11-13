using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
  public  class UiIniParam
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string UserId { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizationCode { get; set; }
        //XmlIgnoreAttribute
        /// <summary>
        /// 验证码
        /// </summary>
        public string AuthCode { get; set; }
    }
}
