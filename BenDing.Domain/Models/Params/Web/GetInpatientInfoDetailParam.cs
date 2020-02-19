using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Web
{
  public  class GetInpatientInfoDetailParam
    { /// <summary>
        /// 用户id
        /// </summary>
       
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
    
        public string UserId { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public  string BusinessId { get; set; }
        
    }
}
