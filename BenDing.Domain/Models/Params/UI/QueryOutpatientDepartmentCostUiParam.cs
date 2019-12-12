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
   public class QueryOutpatientDepartmentCostUiParam:UiInIParam
    {
        /// <summary>
        /// 流水号
        /// </summary>
        [Display(Name = "流水号")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string DocumentNo { get; set; }
    }
}
