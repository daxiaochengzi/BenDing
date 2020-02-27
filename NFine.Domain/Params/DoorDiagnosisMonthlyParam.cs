using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Code;

namespace NFine.Domain.Params
{
   public class DoorDiagnosisMonthlyParam: Pagination
    {/// <summary>
     /// 结算开始时间
     /// </summary>
        public string SettlementStartTime { get; set; }
        /// <summary>
        /// 结算结束时间
        /// </summary>
        public string SettlementEndTime { get; set; }
        /// <summary>
        /// 结算人群
        /// </summary>
        public string PeopleType { get; set; }
    }
}
