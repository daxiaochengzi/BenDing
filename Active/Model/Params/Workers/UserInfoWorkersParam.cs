using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{/// <summary>
    /// 职工获取个人基本资料  
    /// </summary>
    public class UserInfoWorkersParam
    { /// <summary>
        /// 个人IC卡号或身份证号或个人编号  
        /// </summary>
        public string Pi_sfbz { get; set; }
        /// <summary>
        /// 1为公民身份号码 2为医保卡号 3为个人编号  
        /// </summary>
        public string Pi_crbz { get; set; }
        /// <summary>
        /// 所属行政区  
        /// </summary>
        public string Pi_xzqh { get; set; }
    }
}
