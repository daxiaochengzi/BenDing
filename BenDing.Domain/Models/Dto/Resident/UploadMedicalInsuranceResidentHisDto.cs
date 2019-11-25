using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto
{/// <summary>
/// 上传医保信息到his
/// </summary>
   public class UploadMedicalInsuranceResidentHisDto
    {
        /// <summary>
        /// 验证码
        /// </summary>
        [JsonProperty(PropertyName = "验证码")]
        public string AuthCode { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        [JsonProperty(PropertyName = "业务ID")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 医保卡号
        /// </summary>
        [JsonProperty(PropertyName = "医保卡号")]
        public string InsuranceNo { get; set; }
        /// <summary>
        /// 医保总费用
        /// </summary>
        [JsonProperty(PropertyName = "医保总费用")]
        public decimal MedicalInsuranceAllAmount { get; set; }
        /// <summary>
        /// 报销费用
        /// </summary>
        [JsonProperty(PropertyName = "报销费用")]
        public decimal ReimbursementExpensesAmount { get; set; }
        /// <summary>
        /// 自付费用
        /// </summary>
        [JsonProperty(PropertyName = "自付费用")]
        public decimal SelfPayFeeAmount { get; set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        [JsonProperty(PropertyName = "其他信息")]
        public string OtherInfo { get; set; }
    }
}
