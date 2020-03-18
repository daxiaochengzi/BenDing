using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Workers
{
   public class WorkerBirthPreSettlementDto
    {/// <summary>
     /// 发生费用金额
     /// </summary>

        [JsonProperty(PropertyName = "发生费用金额")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 基本统筹支付
        /// </summary>

        [JsonProperty(PropertyName = "基本统筹支付")]
        public decimal BasicOverallPay { get; set; }
        /// <summary>
        /// 补充医疗保险支付金额
        /// </summary>

        [JsonProperty(PropertyName = "补充医疗保险支付金额")]
        public decimal SupplementPayAmount { get; set; }
        /// <summary>
        /// 生育补助
        /// </summary>

        [JsonProperty(PropertyName = "生育补助")]
        public decimal BirthAAllowance { get; set; }
        /// <summary>
        /// 民政救助报销支付金额
        /// </summary>

        [JsonProperty(PropertyName = "民政救助报销支付金额")]
        public decimal CivilAssistancePayAmount { get; set; }
        /// <summary>
        ///  民政重大疾病救助报销支付金额
        /// </summary>

        [JsonProperty(PropertyName = "民政重大疾病救助报销支付金额")]
        public decimal CivilAssistanceSeriousIllnessPayAmount { get; set; }
        /// <summary>
        ///  精准扶贫报销支付金额
        /// </summary>

        [JsonProperty(PropertyName = "精准扶贫报销支付金额")]
        public decimal AccurateAssistancePayAmount { get; set; }
        /// <summary>
        ///  民政优扶报销支付金额
        /// </summary>
        [JsonProperty(PropertyName = "民政优扶报销支付金额")]
        public decimal CivilServicessistancePayAmount { get; set; }
        /// <summary>
        ///  其它支付金额
        /// </summary>

        [JsonProperty(PropertyName = "其它支付金额")]
        public decimal OtherPaymentAmount { get; set; }
        /// <summary>
        ///  备注
        /// </summary>
        [JsonIgnore]
        [JsonProperty(PropertyName = "备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>

        [JsonProperty(PropertyName = "现金支付")]
        public decimal CashPayment { get; set; }
        /// <summary>
        /// 起付金额
        /// </summary>

        [JsonProperty(PropertyName = "起付金额")]
        public decimal PaidAmount { get; set; }
        /// <summary>
        /// 报销合计金额
        /// </summary>
        [JsonIgnore]
        public decimal ReimbursementExpenses { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        [JsonIgnore]
        public string DocumentNo { get; set; }
        /// <summary>
        /// 过程返回值(为1时正常，否则不正常)
        /// </summary>
        [JsonIgnore]

        public string ResultState { get; set; }
        /// <summary>
        /// 系统错误信息
        /// </summary>
        [JsonIgnore]

        public string Msg { get; set; }

    }
}
