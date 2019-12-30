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
        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
     
        public string UserId { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        [Display(Name = "业务id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
      
        public string BusinessId { get; set; }
        /// <summary>
        /// 发起交易的动作ID
        /// </summary>
        [Display(Name = "发起交易的动作ID")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        [XmlIgnore]
        public string TransKey { get; set; }
    }
}
