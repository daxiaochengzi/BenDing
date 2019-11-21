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
     /// 根据病人业务id上传
     /// </summary>
        [Display(Name = "业务id")]
      
        public string BusinessId { get; set; }
        /// <summary>
        /// 根据数据id上传
        /// </summary>
        public List<string> DataIdList { get; set; } = null;
        /// <summary>
        /// 医保类别
        /// </summary>
        [Display(Name = "医保类别")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string InsuranceType { get; set; }

    }
}
