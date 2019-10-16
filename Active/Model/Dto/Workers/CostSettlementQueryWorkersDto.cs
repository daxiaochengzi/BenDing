using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Workers
{/// <summary>
 /// 费用结算查询
 /// </summary>
    public class CostSettlementQueryWorkersDto:IniDto
    {/// <summary>
        /// 基本医疗统筹支付  
        /// </summary>
        public string Po_tczf { get; set; }
        /// <summary>
        /// 补充医疗保险支付金额  
        /// </summary>
        public string Po_bczf { get; set; }
       
        /// <summary>
        /// 专项基金支付金额  
        /// </summary>
        public string Po_zxjj { get; set; }
        /// <summary>
        /// 公务员补贴 
        /// </summary>
        public string Po_gwybt { get; set; }
        /// <summary>
        /// 公务员补助  
        /// </summary>
        public string Po_gwybz { get; set; }
        /// <summary>
        /// 其它支付金额  
        /// </summary>
        public string Po_qtzf { get; set; }
        /// <summary>
        /// 帐户支付  
        /// </summary>
        public string Po_zhzf { get; set; }
        /// <summary>
        /// 现金支付 
        /// </summary>
        public string Po_xjzf { get; set; }
        /// <summary>
        /// 起付金额  
        /// </summary>
        public string Po_qfje { get; set; }
        /// <summary>
        /// 结算日期  
        /// </summary>
        public string Po_jsrq { get; set; }
        /// <summary>
        /// 备注  
        /// </summary>
        public string Po_bz { get; set; }
    }
}
