using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
  public  class DifferentPlacesCostSettlementParam
    {

        /// <summary>
        /// 参保人统筹区划代码
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 就诊记录号
        /// </summary>

        public string aaz217 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// 拟下个人账户金额，用于抵扣个人自付，金额额不能小于0也不能大于个人账户余额，也不能大于结算后的自付总金额
        /// </summary>
        public double bkc142 { get; set; }
        /// <summary>
        /// 费用明细条数
        /// </summary>
        public string nums { get; set; }
        /// <summary>
        /// 明细总额
        /// </summary>
        public string akc264 { get; set; }
        /// <summary>
        /// 结算经办人
        /// </summary>
        public string bkc131 { get; set; }
    }
}
