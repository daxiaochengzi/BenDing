using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
/// 特殊疾病查询
/// </summary>
   public class IdentificationSpecialDiseasesQueryParam
    {/// <summary>
        /// 身份标志
        /// </summary>
        public string PI_SFBZ { get; set; }
        /// <summary>
        /// 传入标志
        /// </summary>
        public string PI_CRBZ { get; set; }
    }
}
