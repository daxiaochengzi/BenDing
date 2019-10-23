using System;
using System.Collections.Generic;
using System.Text;
using BenDing.Domain.Models.Params.Base;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 住院病人信息查询
/// </summary>
   public class InpatientInfoParam: BaseUiIniParam
    {
        [JsonProperty(PropertyName = "身份证号码")]
        public string IdCardNo { get; set; }
        [JsonProperty(PropertyName = "开始时间")]
        public string StartTime { get; set; }
        [JsonProperty(PropertyName = "结束时间")]
        public string EndTime { get; set; }
        [JsonProperty(PropertyName = "状态")]
        public string State { get; set; }
       

    }
}
