using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class OutpatientSettlementCancelParam
    {
        
        /// <summary>
        /// 参保地统筹区编号
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 就诊登记号
        /// </summary>
        public string aaz217 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// 身份证号码(社会保障号码)
        /// </summary>
        public string aac002 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string aac003 { get; set; }
        /// <summary>
        /// 人员医疗结算事件id(就诊结算id)
        /// </summary>
        public string aaz216 { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string bkc131 { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public double akc264 { get; set; }
        /// <summary>
        /// 医保统筹支付合计（不包含个人账户支付）
        /// </summary>
        public double akb068 { get; set; }
        /// <summary>
        /// 个人账户支付
        /// </summary>
        public double akb066 { get; set; }
    }
}
