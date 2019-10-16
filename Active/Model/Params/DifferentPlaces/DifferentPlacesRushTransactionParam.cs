using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentPlacesRushTransactionParam
    {
       
        /// <summary>
        /// 需要被冲正交易的代码
        /// </summary>
        public string jydm { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string jylsh { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string bkc131 { get; set; }
    }
}
