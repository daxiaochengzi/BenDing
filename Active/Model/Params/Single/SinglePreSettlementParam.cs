using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Single
{/// <summary>
 /// 2.2.9.单病种预结算
 /// </summary>
    public class SinglePreSettlementParam
    { /// <summary>
        /// 就诊记录号  
        /// </summary>
        public string PI_AAZ217 { get; set; }
        /// <summary>
        /// 出院日期  
        /// </summary>
        public string PI_CYRQ { get; set; }
    }
}
