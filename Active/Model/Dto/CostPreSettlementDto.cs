using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto
{/// <summary>
/// 
/// </summary>
  public  class CostPreSettlementDto : IniDto
    {
        /// <summary>
        /// 发生费用金额
        /// </summary>
        public string PO_FYZE { get; set; }
        /// <summary>
        /// 基本统筹支付
        /// </summary>
        public string PO_TCZF { get; set; }
        /// <summary>
        /// 补充医疗保险支付金额
        /// </summary>
        public string PO_BCZF { get; set; }
        /// <summary>
        /// 生育补助
        /// </summary>
        public string PO_SYBZ { get; set; }
        /// <summary>
        /// 民政救助报销支付金额
        /// </summary>
        public string PO_MZJZ { get; set; }
        /// <summary>
        /// 民政重大疾病救助报销支付金额
        /// </summary>
        public string PO_MZDBJZ { get; set; }
        /// <summary>
        /// 精准扶贫报销支付金额 
        /// </summary>
        public string PO_JZFP { get; set; }
        /// <summary>
        /// 民政优扶报销支付金额
        /// </summary>
        public string PO_MZYF { get; set; }
        /// <summary>
        /// 其它支付金额
        /// </summary>
        public string PO_QTZF { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string PO_BZ { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public string PO_XJZF { get; set; }
        /// <summary>
        /// 起付金额
        /// </summary>
        public string PO_QFJE { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        public string PO_DJH { get; set; }
        
    }
}
