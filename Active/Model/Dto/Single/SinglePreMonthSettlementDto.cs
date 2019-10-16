using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Single
{/// <summary>
 /// 单病种 精神病住院月结汇总
 /// </summary>
    public class SinglePreMonthSettlementDto:IniDto
    {/// <summary>
     /// 汇总流水号
     /// </summary>
        public string PO_BAE007 { get; set; }
        /// <summary>
        /// 报销人次
        /// </summary>
        public string P0_AKB079 { get; set; }
        /// <summary>
        /// 汇总报销金额合计
        /// </summary>
        public string PO_AKB065 { get; set; }
   
    }
}
