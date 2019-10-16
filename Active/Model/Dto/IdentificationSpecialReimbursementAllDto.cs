using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto
{
   public class IdentificationSpecialReimbursementAllDto : IniDto
    { /// <summary>
        /// 汇总流水号  
        /// </summary>
        public string PO_BAE007 { get; set; }
        /// <summary>
        /// 报销人次  
        /// </summary>
        public string po_AKC602 { get; set; }
        /// <summary>
        /// 汇总报销金额合计  
        /// </summary>
        public string PO_AKB066 { get; set; }
      
    }
}
