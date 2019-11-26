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
   public class UiInIDataParam
    {/// <summary>
        /// 用户id
        /// </summary>
        [JsonProperty(PropertyName = "UserId")]
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
        [JsonIgnore]
        public string UserId { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        [JsonProperty(PropertyName = "业务id")]
        [Display(Name = "业务id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
        [JsonIgnore]
        public string BusinessId { get; set; }
        /// <summary>
        /// 发起交易的动作ID
        /// </summary>
        [JsonProperty(PropertyName = "发起交易的动作ID")]
        [Display(Name = "发起交易的动作ID")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
        [JsonIgnore]
        public Guid TransactionId { get; set; }
    }
}
