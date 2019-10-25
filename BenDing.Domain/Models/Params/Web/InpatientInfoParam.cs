using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using BenDing.Domain.Models.Params.Base;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 住院病人信息查询
/// </summary>
   public class InpatientInfoParam: BaseUiIniParam
    {
        [Display(Name = "身份证号码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [JsonProperty(PropertyName = "身份证号码")]
        [XmlIgnoreAttribute]
        public string IdCardNo { get; set; }
        [JsonProperty(PropertyName = "开始时间")]
        [XmlIgnoreAttribute]
        [Display(Name = "开始时间")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string StartTime { get; set; }
        [JsonProperty(PropertyName = "结束时间")]
        [XmlIgnoreAttribute]
        [Display(Name = "结束时间")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string EndTime { get; set; }
        [XmlIgnoreAttribute]
        [JsonProperty(PropertyName = "状态")]
        [Display(Name = "状态")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public string State { get; set; }
       

    }
}
