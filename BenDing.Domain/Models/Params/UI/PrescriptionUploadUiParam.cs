using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{
   public class PrescriptionUploadUiParam:UiInIParam
    {/// <summary>
    /// 业务id
    /// </summary>
        [Display(Name = "业务id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 数据id
        /// </summary>
        public List<string> DataIdList { get; set; }
        /// <summary>
        /// 医保类别
        /// </summary>
        [Display(Name = "医保类别")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string InsuranceType { get; set; }

    }
}
