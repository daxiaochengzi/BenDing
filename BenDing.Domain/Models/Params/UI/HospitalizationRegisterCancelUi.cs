using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{
   public class HospitalizationRegisterCancelUi: UiInIParam
    {
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string businessId { get; set; } 
    }
}
