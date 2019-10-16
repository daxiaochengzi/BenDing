using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{
  public  class IdentificationSpecialReimbursementQueryParam
    {/// <summary>
     /// 身份标志
     /// </summary>
        public string PI_SFBZ { get; set; }
        /// <summary>
        /// 传入标志
        /// </summary>
        public string PI_CRBZ { get; set; }
        /// <summary>
        /// 报销流水号
        /// </summary>
        public string PI_AKC604 { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string PI_AAE030 { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string PI_AAE031 { get; set; }
  

    }
}
