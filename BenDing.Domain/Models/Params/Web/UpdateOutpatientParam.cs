using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Web
{
   public class UpdateOutpatientParam
    {   /// <summary>
    /// id
    /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 交易id
        /// </summary>
        public string SettlementTransactionId { get; set; } 
        /// <summary>
        /// 结算取消交易id
        /// </summary>
        public string SettlementCancelTransactionId { get; set; }
    }
}
