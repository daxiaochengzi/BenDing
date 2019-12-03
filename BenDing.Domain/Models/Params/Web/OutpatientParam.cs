using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace BenDing.Domain.Models.Params.Web
{/// <summary>
/// 获取门诊病人信息
/// </summary>
  public  class OutpatientParam
    {
        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [JsonProperty(PropertyName = "身份证号码")]
        public string IdCardNo { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [JsonProperty(PropertyName = "机构编码")]
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty(PropertyName = "开始时间")]
        public string StartTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty(PropertyName = "结束时间")]
        public string EndTime { get; set; }
     
    }
}
