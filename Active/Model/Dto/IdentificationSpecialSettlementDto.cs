using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto
{
   public class IdentificationSpecialSettlementDto : IniDto
    {/// <summary>
     /// 报销流水号
     /// </summary>
        public string PO_AKC604 { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public string PO_CKC501 { get; set; }
        /// <summary>
        /// 不合规费用
        /// </summary>
        public string PO_CKC510 { get; set; }
        /// <summary>
        /// 个人现金支付
        /// </summary>
        public string PO_CKC511 { get; set; }
        /// <summary>
        /// 进入统筹费用
        /// </summary>
        public string PO_CKC705 { get; set; }
        /// <summary>
        /// 报销比例
        /// </summary>
        public string PO_CKA523 { get; set; }
        /// <summary>
        /// 超封顶线金额
        /// </summary>
        public string PO_CKC509 { get; set; }
        /// <summary>
        /// 超限价金额
        /// </summary>
        public string PO_CKE848 { get; set; }
        /// <summary>
        /// 报销金额
        /// </summary>
        public string PO_AKB066 { get; set; }
        /// <summary>
        /// 门特三补充保险报销金额
        /// </summary>
        public string PO_CKC503 { get; set; }
        /// <summary>
        /// 个人账户余额
        /// </summary>
        public string PO_AAE240 { get; set; }
        /// <summary>
        /// 本次报销后可报余额
        /// </summary>
        public string PO_AAE241 { get; set; }
        /// <summary>
        /// 医保结算人群
        /// </summary>
        public string PO_CKA549 { get; set; }
   
    

    }
}
