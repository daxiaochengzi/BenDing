using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Web
{
    /// <summary>
    /// 获取门诊病人明细
    /// </summary>
  public  class OutpatientDetailParam
    {
        /// <summary>
        /// 验证码
        /// </summary>
      
        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty(PropertyName = "业务ID")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        [JsonProperty(PropertyName = "门诊号")]
        public string OutpatientNo { get; set; }
    }
}
