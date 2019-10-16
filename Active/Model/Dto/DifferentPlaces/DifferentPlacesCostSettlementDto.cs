using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.DifferentPlaces
{
   public class DifferentPlacesCostSettlementDto:IniDto
    {
        /// <summary>
        /// 生育保险支付
        /// </summary>
        public string po_sybxzf { get; set; }
        /// <summary>
        /// 不享受待遇原因
        /// </summary>
        public string po_bxsdyyy { get; set; }
        /// <summary>
        /// 公务员支付
        /// </summary>
        public string po_gwyzf { get; set; }
        /// <summary>
        /// 新农合支付
        /// </summary>
        public string po_xnhzf { get; set; }
        /// <summary>
        /// 基它险种支付
        /// </summary>
        public string po_qtxzzf { get; set; }
        /// <summary>
        /// 结算单据号
        /// </summary>
        public string po_djh { get; set; }

        /// <summary>
        /// 居民统筹支付
        /// </summary>
        public string po_jmtczf { get; set; }
        /// <summary>
        /// 险种
        /// </summary>
        public string po_xzlx { get; set; }

        /// <summary>
        /// 享受待遇标识
        /// </summary>
        public string po_xsdybs { get; set; }
        /// <summary>
        /// 超限价自付
        /// </summary>
        public string po_cxjzf { get; set; }
        /// <summary>
        /// 结算后账户余额
        /// </summary>
        public string po_jshzhye { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public string po_fyze { get; set; }
        /// <summary>
        /// 账户最高可支付金额
        /// </summary>
        public string po_zhzgkzf { get; set; }
        /// <summary>
        /// 基本统筹支付
        /// </summary>
        public string po_tczf { get; set; }
        /// <summary>
        /// 起付金额
        /// </summary>
        public string po_qfje { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public string po_xjzf { get; set; }
        /// <summary>
        /// 账户支付
        /// </summary>
        public string po_zhzf { get; set; }
        /// <summary>
        /// 先自付金额
        /// </summary>
        public string po_xzfje { get; set; }
        /// <summary>
        /// 纯自费金额
        /// </summary>
        public string po_czfje { get; set; }
        /// <summary>
        /// 报销情况说明
        /// </summary>
        public string po_bxqksm { get; set; }
        /// <summary>
        /// 进入报销范围金额
        /// </summary>
        public string po_jrbxje { get; set; }
        /// <summary>
        /// 社保支付合计
        /// </summary>
        public string po_sbzfhj { get; set; }
        /// <summary>
        /// 补充医疗保险支付
        /// </summary>
        public string po_bcylzf { get; set; }
        /// <summary>
        /// 居民大病保险支付
        /// </summary>
        public string po_jmdbzf { get; set; }
    }
}
