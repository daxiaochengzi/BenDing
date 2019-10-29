using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.UI
{
  public  class InpatientInfoUiParam: UiInIParam
    {
        [Display(Name = "身份证号码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
      
     
        public string IdCardNo { get; set; }
        [JsonProperty(PropertyName = "开始时间")]
        
        [Display(Name = "开始时间")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string StartTime { get; set; }
        [JsonProperty(PropertyName = "结束时间")]
    
        [Display(Name = "结束时间")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string EndTime { get; set; }
      
        [JsonProperty(PropertyName = "状态")]
        [Display(Name = "状态")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string State { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
       
        [Display(Name = "业务id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [JsonIgnore]
        public  string BusinessId { get; set; }
    }
}
