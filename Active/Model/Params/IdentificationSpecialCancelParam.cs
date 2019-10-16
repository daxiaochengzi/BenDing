using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{
   public class IdentificationSpecialCancelParam
    {/// <summary>
        /// 报销流水号  
        /// </summary>
        public string PI_AKC604 { get; set; }
        /// <summary>
        /// 取消原因说明
        /// </summary>
        public string PI_AAE013 { get; set; }
    }
}
