using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{
  public  class CostSettlementCancelWorkersParam
    {/// <summary>
        /// 医疗机构号  
        /// </summary>
        public string Pi_fwjgh { get; set; }
        /// <summary>
        /// 行政区划  
        /// </summary>
        public string Pi_xzqh { get; set; }
        /// <summary>
        /// 住院号  
        /// </summary>
        public string Pi_zyh { get; set; }
        /// <summary>
        /// 登记号  
        /// </summary>
        public string Pi_djh { get; set; }
        /// <summary>
        /// 取消程度(1取消结算2删除资料)  
        /// </summary>
        public string Pi_qxcd { get; set; }
        /// <summary>
        /// 经办人  
        /// </summary>
        public string Pi_jbr { get; set; }
    }
}
