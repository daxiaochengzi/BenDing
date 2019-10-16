using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Single
{/// <summary>
 /// 2.2.26.门诊诊查记录查询
 /// </summary>
    public class OutpatientConsultationFeeQueryParam
    {/// <summary>
        /// 证件号码  
        /// </summary>
        public string PI_AAC001 { get; set; }
        /// <summary>
        /// 业务流水号  
        /// </summary>
        public string PI_BAE007 { get; set; }
    }
}
