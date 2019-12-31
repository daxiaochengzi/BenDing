using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 
/// </summary>
   public class GetOutpatientUiParam: UiBaseDataParam
    {/// <summary>
     /// 身份证号码
     /// </summary>
        [Display(Name = "身份证号码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string IdCardNo { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string StartTime { get; set; }

    }
}
