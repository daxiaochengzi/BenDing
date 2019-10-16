using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Basiclevel.Param
{
   public class InpatientInfoDetailQueryBasiclevelParam
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
