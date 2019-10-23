using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Web
{
    public class UserInfoParam
    {
        [JsonProperty(PropertyName = "用户名")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "密码")]
        public string Pwd { get; set; }
        [JsonProperty(PropertyName = "厂商编号")]
        public string ManufacturerNumber { get; set; }
    }
}
