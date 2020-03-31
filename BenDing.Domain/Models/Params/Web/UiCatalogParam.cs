using BenDing.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using BenDing.Domain.Models.Params.UI;


namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 
/// </summary>
  public  class UiCatalogParam: UiInIParam
    {/// <summary> 
     /// 目录类别编 (0中药;1西药;2诊疗;3耗材)
     /// </summary>
        [Display(Name = "目录类别编")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string CatalogType { get; set; }
    }
}
