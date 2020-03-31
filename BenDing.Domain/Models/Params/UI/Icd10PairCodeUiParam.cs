using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{
  public  class Icd10PairCodeUiParam: UiInIDataParam
    {  
        /// <summary>
        /// 基层疾病id
        /// </summary>
        [Display(Name = "基层疾病id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string DiseaseId { get; set; }
        /// <summary>
        /// 医保编码
        /// </summary>
        [Display(Name = "医保编码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string ProjectCode { get; set; }
        /// <summary>
        /// 医保项目名称
        /// </summary>
        [Display(Name = "医保项目名称")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string ProjectName { get; set; }
    }
}
