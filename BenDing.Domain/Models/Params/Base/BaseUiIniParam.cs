using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Base
{
  public  class BaseUiIniParam
    {/// <summary>
     /// 验证码
     /// </summary>
        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [JsonProperty(PropertyName = "机构编码")]
        public string OrganizationCode { get; set; }
    }
}
