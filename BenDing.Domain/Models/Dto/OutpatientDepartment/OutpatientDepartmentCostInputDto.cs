using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.OutpatientDepartment
{
   public class OutpatientDepartmentCostInputDto
    {/// <summary>
     /// 业务流水号
     /// </summary>
        [JsonIgnore]
        [JsonProperty(PropertyName = "业务流水号")]
        public string DocumentNo { get; set; }
        /// <summary>
        /// 报销支付
        /// </summary>

        [JsonProperty(PropertyName = "报销支付")]
        public decimal ReimbursementExpensesAmount { get; set; }

        /// <summary>
        /// 个人自付
        /// </summary>

        [JsonProperty(PropertyName = "个人自付")]
        public decimal SelfPayFeeAmount { get; set; }

        /// <summary>
        /// 报销说明备注
        /// </summary>

        [JsonProperty(PropertyName = "报销说明备注")]
        [JsonIgnore]
        public string Remark { get; set; }
    }
}
