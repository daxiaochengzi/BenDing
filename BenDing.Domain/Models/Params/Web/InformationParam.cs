using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Web
{
   public class InformationParam
    {/// <summary>
     /// 验证码
     /// </summary>
        [JsonProperty(PropertyName = "验证码")]
        public  string AuthCode { get; set; }
        /// <summary>
        /// 目录类型
        /// </summary>
        [JsonProperty(PropertyName = "目录类型")]
        public  string DirectoryType { get; set; }
        /// <summary>
        /// 目录名称
        /// </summary>
        [JsonProperty(PropertyName = "目录名称")]
        public  string DirectoryName { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [JsonProperty(PropertyName = "机构编码")]
        public  string OrganizationCode { get; set; }
    }
}
