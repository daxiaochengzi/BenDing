using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{/// <summary>
    /// 已经上传处方明细查询  
    /// </summary>
   public class PrescriptionDetailQueryWorkersParam
    { /// <summary>
        /// 在医保上的就诊记录号  
        /// </summary>
        public string Pi_jzjlh { get; set; }
        /// <summary>
        /// 行政区划  
        /// </summary>
        public string Pi_xzqh { get; set; }
    }
}
