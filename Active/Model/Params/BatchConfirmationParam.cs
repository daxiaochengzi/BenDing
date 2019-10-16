using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
/// 批次确认
/// </summary>
   public class BatchConfirmationParam
    { /// <summary>
        /// 住院号
        /// </summary>
        public string PI_ZYH { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string PI_PCH { get; set; }
      
        /// <summary>
        /// 确认类型
        /// </summary>
        public string PI_QRLX { get; set; }
        /// <summary>
        ///经办人
        /// </summary>
        public string PI_JBR { get; set; }
    }
}
