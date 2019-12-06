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
        /// <summary>
        /// 业务id
        /// </summary>
        [Display(Name = "业务id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
        public  string BusinessId { get; set; }
    }
}
