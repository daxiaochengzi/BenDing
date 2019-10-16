using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Single
{/// <summary>
 /// 2.2.24.门诊诊查费录入及结算
 /// </summary>
    public class OutpatientConsultationFeeSettlementDto:IniDto
    {/// <summary>
        /// 业务流水号  
        /// </summary>
        public string PO_BAE007 { get; set; }
        /// <summary>
        /// 报销支付  
        /// </summary>
        public string PO_TCZF { get; set; }
        /// <summary>
        /// 个人自付  
        /// </summary>
        public string PO_XJZF { get; set; }
        /// <summary>
        /// 报销说明备注  
        /// </summary>
        public string PO_SMBZ { get; set; }
    }
}
