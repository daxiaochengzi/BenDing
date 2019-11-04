
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.Web
{
  public  class SaveXmlData
    {/// <summary>
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
        /// 入参
        /// </summary>
        [JsonProperty(PropertyName = "入参")]
        public string IntoParam { get; set; }
        /// <summary>
        /// 出参
        /// </summary>
        [JsonProperty(PropertyName = "出参")]
        public string BackParam { get; set; }
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
        /// 医保返回业务号
        /// </summary>
        [JsonProperty(PropertyName = "医保返回业务号")]
        public string MedicalInsuranceBackNum { get; set; }
        /// <summary>
        /// 医保交易码
        /// </summary>
        [JsonProperty(PropertyName = "医保交易码")]
        public string MedicalInsuranceCode { get; set; }
        ///// <summary>
        ///// 备注
        ///// </summary>
        //[JsonIgnore]
        //public string Remark { get; set; }
        ///// <summary>
        ///// 身份证
        ///// </summary>
        //[JsonIgnore]
        //public string IDCard { get; set; }
        

    }
}
