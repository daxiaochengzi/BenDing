using BenDing.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;


namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 
/// </summary>
  public  class UiCatalogParam
    {/// <summary> 
        /// 目录类别编
        /// </summary>
        [Display(Name = "目录类别编")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public CatalogTypeEnum CatalogType { get; set; }
    }
}
