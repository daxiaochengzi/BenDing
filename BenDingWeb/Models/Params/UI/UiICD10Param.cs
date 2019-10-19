using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalInsurance.Domain.Models.Enums;

namespace BenDingWeb.Models.Params.UI
{
   public class UiICD10Param
    {/// <summary> 
        /// 目录类别编
        /// </summary>
        [Display(Name = "目录类别编")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public CatalogTypeEnum CatalogType { get; set; }
    }
}
