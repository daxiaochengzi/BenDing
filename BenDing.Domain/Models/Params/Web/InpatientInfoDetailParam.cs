
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Web
{
   public class InpatientInfoDetailParam
    {

        [JsonProperty(PropertyName = "业务ID")]
        public string BusinessId { get; set; }
        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        [JsonProperty(PropertyName = "住院号")]
        public string HospitalizationNo { get; set; }
        [JsonProperty(PropertyName = "开始时间")]
        public string StartTime { get; set; }
        [JsonProperty(PropertyName = "结束时间")]
        public string EndTime { get; set; }
        [JsonProperty(PropertyName = "状态")]
        public string State { get; set; }
    }
}
