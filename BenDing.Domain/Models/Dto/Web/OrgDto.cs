using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
   public class OrgDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 医院名称
        /// </summary>
        [JsonProperty(PropertyName = "医院名称")]
        public string HospitalName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [JsonProperty(PropertyName = "地址")]
        public string HospitalAddr { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [JsonProperty(PropertyName = "联系电话")]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [JsonProperty(PropertyName = "邮政编码")]
        public string PostalCode { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [JsonProperty(PropertyName = "联系人")]
        public string ContactPerson { get; set; }
    }
}
