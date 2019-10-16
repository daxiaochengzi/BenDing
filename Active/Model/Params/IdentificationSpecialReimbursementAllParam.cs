using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
 /// 特殊疾病报销汇总
 /// </summary>
    public class IdentificationSpecialReimbursementAllParam
    {/// <summary>
        /// 人员类别  
        /// </summary>
        public string PI_CKA549 { get; set; }
        /// <summary>
        /// 汇总开始日期  
        /// </summary>
        public string PI_KSRQ { get; set; }
        /// <summary>
        /// 汇总截止日期  
        /// </summary>
        public string PI_JZRQ { get; set; }

    }
}
