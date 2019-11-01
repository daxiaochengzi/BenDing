using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
   public class ThreeCataloguePairCodeUploadDto
    {
        [JsonProperty(PropertyName = "版本")]
        public string VersionNumber { get; set; }
        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        [JsonProperty(PropertyName = "操作人员姓名")]
        public string UserName { get; set; }
        /// <summary>
        /// 撤销状态
        /// </summary>
        [JsonProperty(PropertyName = "撤销状态")]
        public string CanCelState { get; set; }
    
    }
}
