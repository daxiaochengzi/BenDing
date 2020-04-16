using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.Params
{
   public class SaveDoorDiagnosisMonthlyParam
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
        /// <summary>
        /// 结算回参
        /// </summary>
        public  string SettlementJson { get; set; }
    }
}
