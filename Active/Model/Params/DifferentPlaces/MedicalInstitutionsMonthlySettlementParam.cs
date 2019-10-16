using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class MedicalInstitutionsMonthlySettlementParam
    {
     
        /// <summary>
        /// 申请地统筹区编号
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 清算类别
        /// </summary>
        public string bka015 { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string aae030 { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string aae031 { get; set; }
        /// <summary>
        /// 医疗总费用
        /// </summary>
        public string akc264 { get; set; }
        /// <summary>
        /// 个人账户支付
        /// </summary>
        public string akb066 { get; set; }
        /// <summary>
        /// 统筹支付金额合计
        /// </summary>
        public string akb068 { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>

        public string bkc131 { get; set; }
    }
}
