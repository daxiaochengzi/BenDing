using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Workers
{
   public class WorkerStrokeCardDto
    {/// <summary>
    /// 流水号
    /// </summary>
        public string DocumentNo { get; set; }
        /// <summary>
        /// 自费金额
        /// </summary>
        public  decimal SelfPayFeeAmount { get; set; }
        /// <summary>
        /// 报销金额
        /// </summary>
        public decimal ReimbursementExpensesAmount { get; set; }

    }
}
