using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Workers
{ /// <summary>
    /// 住院登记  
    /// </summary>
    public class HospitalizationRegisterWorkersDto:IniDto
    {/// <summary>
        /// 社保住院号  
        /// </summary>
        public string Po_zyh { get; set; }
        /// <summary>
        /// 审批编号 
        /// </summary>
        public string Po_spbh { get; set; }
        /// <summary>
        /// 本年已住院次数  
        /// </summary>
        public string Po_bnyzycs { get; set; }
        /// <summary>
        /// 本年统筹已支付金额  
        /// </summary>
        public string Po_bntcyzfje { get; set; }
        /// <summary>
        /// 本年统筹可支付金额  
        /// </summary>
        public string Po_bntckzfje { get; set; }
    }
}
