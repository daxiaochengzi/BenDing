using System;
using System.Collections.Generic;
using System.Text;

namespace BenDing.Domain.Models.Params.Web
{
   public class InpatientInfoDetailQueryParam
    {/// <summary>
    /// 明细id
    /// </summary>
        public List<string> IdList { get; set; } 
        /// <summary>
        /// 住院号
        /// </summary>
        public string HospitalizationNumber { get; set; }
    }
}
