using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MedicalInsurance.Domain.Models.Enums;

namespace BenDingWeb.Models.Params
{
  public  class UiCatalogParam
    {/// <summary> 
        /// 目录类别编
        /// </summary>
        [Display(Name = "目录类别编")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public CatalogTypeEnum CatalogType { get; set; }
    }
}
