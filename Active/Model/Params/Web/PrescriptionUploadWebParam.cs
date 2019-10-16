using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Web
{
   public class PrescriptionUploadWebParam
    {/// <summary>
    /// 明细id(每次最多49条)
    /// </summary>
        public List<string> BusinessIdDetailList { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public string RowNumber { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string HospitalizationNumber { get; set; }
        /// <summary>
        /// 医保住院号
        /// </summary>
        public string PI_ZHY { get; set; }
        
    }
}
