using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Base
{
  public  class BaseUiIniParam
    {/// <summary>
     /// 验证码
     /// </summary>
        [JsonProperty(PropertyName = "验证码")]
        [Display(Name = "验证码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
        public string AuthCode { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty(PropertyName = "机构编码")]
        [Display(Name = "机构编码")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [JsonProperty(PropertyName = "用户id")]
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
        [JsonIgnore]
        public string UserId { get; set; }
    }
}
