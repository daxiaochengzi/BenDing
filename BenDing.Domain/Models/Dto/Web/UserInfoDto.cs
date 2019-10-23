using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 用户信息
/// </summary>
  
    public class UserInfoDto
    {
        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        [JsonProperty(PropertyName = "职员ID")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "职员姓名")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "机构编码")]
        public string OrganizationCode { get; set; }
        [JsonProperty(PropertyName = "机构名称")]
        public string OrganizationName { get; set; }
        [JsonProperty(PropertyName = "管辖区划")]
        public List<string> RegionCodeList { get; set; }
    }
}
