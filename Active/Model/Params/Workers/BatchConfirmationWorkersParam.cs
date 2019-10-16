using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{/// <summary>
    /// 费用批次未注册确认信息查询  
    /// </summary>
    public class BatchConfirmationWorkersParam
    {
        /// <summary>
        /// 在医保上的住院号  
        /// </summary>
        public string Pi_jzjlh { get; set; }
        /// <summary>
        /// 行政区划  
        /// </summary>
        public string Pi_xzqh { get; set; }
        /// <summary>
        /// 批次号(查询结果)   
        /// </summary>
        public string Pi_pch { get; set; }
        /// <summary>
        /// 确认类型 1确认，2取消  
        /// </summary>
        public string Pi_qrlx { get; set; }
        /// <summary>
        /// 经办人  
        /// </summary>
        public string Pi_jbr { get; set; }
    }
}
