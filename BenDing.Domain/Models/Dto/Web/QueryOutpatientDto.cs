using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 
/// </summary>
   public class QueryOutpatientDto: BaseOutpatientInfoDto
    {/// <summary>
    /// 取消结算交易id
    /// </summary>
        public string SettlementCancelTransactionId { get; set; }
        /// <summary>
        /// 结算交易id
        /// </summary>

        public string SettlementTransactionId { get; set; }
    }
}
