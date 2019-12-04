using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{/// <summary>
 /// 用户信息
 /// </summary>

    public class UserInfoJsonDto
    {    /// <summary>
         /// 验证码
         /// </summary>
        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        /// <summary>
        /// 职员ID
        /// </summary>
        [JsonProperty(PropertyName = "职员ID")]
        public string UserId { get; set; }
        /// <summary>
        /// 职员姓名
        /// </summary>
        [JsonProperty(PropertyName = "职员姓名")]
        public string UserName { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [JsonProperty(PropertyName = "机构编码")]
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        [JsonProperty(PropertyName = "机构名称")]
        public string OrganizationName { get; set; }
        /// <summary>
        /// 管辖区划
        /// </summary>
        [JsonProperty(PropertyName = "管辖区划")]
        public List<string> RegionCodeList { get; set; }
    }
}
