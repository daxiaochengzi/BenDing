using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
  public  class MedicalInsuranceXmlDto
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [JsonProperty(PropertyName = "业务ID")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 发起交易的动作ID
        /// </summary>
        [JsonProperty(PropertyName = "发起交易的动作ID")]
        public string TransactionId { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>

        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        /// <summary>
        /// 操作人员ID
        /// </summary>

        [JsonProperty(PropertyName = "操作人员ID")]
        public string UserId { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        [JsonProperty(PropertyName = "机构ID")]

        public string OrganizationCode { get; set; }
        /// <summary>
        /// 医保交易码
        /// </summary>
        [JsonProperty(PropertyName = "医保交易码")]
        public string HealthInsuranceNo { get; set; }

    }
}
