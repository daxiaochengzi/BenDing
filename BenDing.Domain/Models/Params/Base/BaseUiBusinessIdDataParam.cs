using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Base;

namespace BenDing.Domain.Models.Params.Base
{/// <summary>
    /// 根据业务id查询数据
    /// </summary>
    public class BaseUiBusinessIdDataParam 
    { /// <summary>
      /// 用户id
      /// </summary>

        [Display(Name = "用户id")]
        [Required(ErrorMessage = "{0}不能为空!!!")]

        public string UserId { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
      
        [Display(Name = "业务ID")]
        [Required(ErrorMessage = "{0}不能为空!!!")]

        public string BusinessId { get; set; }
        /// <summary>
        /// 发起交易的动作ID
        /// </summary>
        [Display(Name = "发起交易的动作ID")]
        [Required(ErrorMessage = "{0}不能为空!!!")]
       
        public string TransKey { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public List<string> DataIdList { get; set; } = null;
    }
}
