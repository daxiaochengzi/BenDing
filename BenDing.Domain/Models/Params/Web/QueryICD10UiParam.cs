using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Web
{
   public class QueryICD10UiParam
    {/// <summary>
     /// 查询关键字
     /// </summary>
        [Display(Name = "查询关键字")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string Search{ get; set; }
        /// <summary>
        /// 疾病编码
        /// </summary>
        public string DiseaseCoding { get; set; }


    }
}
