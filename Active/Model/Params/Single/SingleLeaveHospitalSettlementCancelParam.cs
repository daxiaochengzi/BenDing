using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Single
{/// <summary>
 /// 单病种出院结算取消
 /// </summary>
    public class SingleLeaveHospitalSettlementCancelParam
    {/// <summary>
        /// 就诊记录号  
        /// </summary>
        public string PI_AAZ217 { get; set; }
        /// <summary>
        /// 结算记录号  
        /// </summary>
        public string PI_AAZ216 { get; set; }
        /// <summary>
        /// 操作员  
        /// </summary>
        public string PI_JBR { get; set; }
    }
}
