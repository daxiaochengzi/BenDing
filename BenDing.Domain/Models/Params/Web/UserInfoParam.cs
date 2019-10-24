using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Web
{
    public class UserInfoParam
    {/// <summary>
     /// 用户名
     /// </summary>
        [JsonProperty(PropertyName = "用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty(PropertyName = "密码")]
        public string Pwd { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        [JsonProperty(PropertyName = "厂商编号")]
        public string ManufacturerNumber { get; set; }
    }
}
