using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{/// <summary>
    /// 住院费用预结算  
    /// </summary>
    public class CostPreSettlementWorkersParam
    {
        /// <summary>
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
        /// <summary>
        /// 是否计住院次数  
        /// </summary>
        public string Pi_cszl { get; set; }
        /// <summary>
        /// 操作员  
        /// </summary>
        public string Pi_czy { get; set; }
        /// <summary>
        /// 出院日期  
        /// </summary>
        public string Pi_cyrq { get; set; }
    }
}
