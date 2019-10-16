using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{
   public class BatchUnConfirmationWorkersParam
    { /// <summary>
        /// 在医保上的住院号  
        /// </summary>
        public string Pi_jzjlh { get; set; }
        /// <summary>
        /// 行政区划  
        /// </summary>
        public string Pi_xzqh { get; set; }
    }
}
