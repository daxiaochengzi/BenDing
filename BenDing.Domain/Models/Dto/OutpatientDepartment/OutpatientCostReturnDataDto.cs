using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;

namespace BenDing.Domain.Models.Dto.OutpatientDepartment
{
   public class OutpatientCostReturnDataDto
    {/// <summary>
        /// 业务流水号
        /// </summary>

       
        public string DocumentNo { get; set; }
        /// <summary>
        /// 报销支付
        /// </summary>

      
        public decimal ReimbursementExpensesAmount { get; set; }

        /// <summary>
        /// 个人自付
        /// </summary>

       
        public decimal SelfPayFeeAmount { get; set; }

        /// <summary>
        /// 报销说明备注
        /// </summary>

        public string Remark { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public List<PayMsgData> PayMsg { get; set; }
    }
}
