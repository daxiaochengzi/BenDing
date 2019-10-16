using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Single
{/// <summary>
 /// 单病种 精神病住院登记
 /// </summary>
    public class SingleHospitalizationRegisterDto: IniDto
    {/// <summary>
     /// 社保住院号  
     /// </summary>
        public string PO_ZYH { get; set; }
        /// <summary>
        /// 本年统筹可支付金额  
        /// </summary>
        public string PO_BNTCKZFJE { get; set; }
        /// <summary>
        /// 本年已住院次数  
        /// </summary>
        public string PO_BNYZYCS { get; set; }
        
    }
}
