using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// Ui参数UserId
/// </summary>
   public class UiInIParam
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [JsonProperty(PropertyName = "UserId")]
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
        [JsonIgnore]
        public string UserId { get; set; }
       
        

    }
}
