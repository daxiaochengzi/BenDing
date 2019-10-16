using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
/// 处方删除
/// </summary>
  public  class PrescriptionDeleteParam
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string PI_ZHY { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string PI_PCH { get; set; }
    }
}
