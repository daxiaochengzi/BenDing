using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Resident
{
   public class SettlementDto
    {
        /// <summary>
        /// 合计金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 报销金额
        /// </summary>
        public decimal ReimbursementExpenses { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
      
        public decimal CashPayment { get; set; }
        /// <summary>
        /// 支付信息
        /// </summary>
        public List<PayMsgData> PayMsg { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class PayMsgData
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
