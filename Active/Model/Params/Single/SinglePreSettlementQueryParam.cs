using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Single
{
   public class SinglePreSettlementQueryParam
    {
        /// <summary>
        /// 就诊记录号  
        /// </summary>
        public string PI_AAZ217 { get; set; }
        /// <summary>
        /// 结算记录号  
        /// </summary>
        public string PI_AAZ216 { get; set; }
        /// <summary>
        /// 证件号码  
        /// </summary>
        public string PI_AAC002 { get; set; }
    }
}
