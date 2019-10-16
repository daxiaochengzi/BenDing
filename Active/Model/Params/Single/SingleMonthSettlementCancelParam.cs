using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Single
{/// <summary>
 /// 单病种 精神病月结汇总取消
 /// </summary>
    public class SingleMonthSettlementCancelParam
    {
        /// <summary>
        /// 汇总业务流水号  
        /// </summary>
        public string PI_BAE007 { get; set; }
        /// <summary>
        /// 人员类别  
        /// </summary>
        public string PI_CKA549 { get; set; }
        /// <summary>
        /// 汇总类别  20-单病种住院月结21-精神病住院结算22-门诊诊查费结算
        /// </summary>
        public string PI_CKE544 { get; set; }
    }
}
