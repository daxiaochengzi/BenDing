using BenDing.Domain.Models.Params.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Resident
{
   public class HospitalizationRegisterCancelParam: UiBaseDataParam
    {/// <summary>
     /// 医保住院号
     /// </summary>
        [Display(Name = "医保住院号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string HospitalizationNo { get; set; }
    }
}
