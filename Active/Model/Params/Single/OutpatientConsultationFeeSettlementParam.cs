using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Single
{/// <summary>
 /// 2.2.24.门诊诊查费录入及结算
 /// </summary>
    public class OutpatientConsultationFeeSettlementParam
    {/// <summary>
        /// 身份标志  
        /// </summary>
        public string PI_SFBZ { get; set; }
        /// <summary>
        /// 传入标志  1为公民身份号码 2为个人编号
        /// </summary>
        public string PI_CRBZ { get; set; }
        /// <summary>
        /// 门诊诊查费总费用  
        /// </summary>
        public double PI_ZCFY { get; set; }
        /// <summary>
        /// 经办人  
        /// </summary>
        public string PI_JBR { get; set; }
    }
}
