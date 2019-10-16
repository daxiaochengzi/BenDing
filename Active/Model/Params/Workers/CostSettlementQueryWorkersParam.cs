using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{/// <summary>
    /// 查询费用结算结果  
    /// </summary>
    public class CostSettlementQueryWorkersParam
    { /// <summary>
        /// 医疗机构号  
        /// </summary>
        public string Pi_fwjgh { get; set; }
        /// <summary>
        /// 住院号  
        /// </summary>
        public string Pi_zyh { get; set; }
        /// <summary>
        /// 行政区划  
        /// </summary>
        public string Pi_xzqh { get; set; }
    }
}
