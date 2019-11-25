using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 
/// </summary>
   public class PrescriptionUploadAutomaticUiParam
    {/// <summary>
     /// 是否当日上传
     /// </summary>
        public Boolean IsTodayUpload { get; set; } = true;
        /// <summary>
        /// 用户id
        /// </summary>
       
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string UserId { get; set; }
    }
}
