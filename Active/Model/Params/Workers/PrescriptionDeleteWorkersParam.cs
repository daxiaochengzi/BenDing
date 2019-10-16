using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{/// <summary>
    /// 已经上传处方明细删除  
    /// </summary>
    public class PrescriptionDeleteWorkersParam
    {/// <summary>
        /// 在医保上的就诊记录号  
        /// </summary>
        public string Pi_jzjlh { get; set; }
        /// <summary>
        /// 批次号(查询结果)   
        /// </summary>
        public string Pi_pch { get; set; }
        /// <summary>
        /// 行政区划  
        /// </summary>
        public string Pi_xzqh { get; set; }
    }
}
