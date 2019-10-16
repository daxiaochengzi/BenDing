using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.DifferentPlaces
{
   public class OutpatientSimulatedSettlementDto:IniDto
    {
        /// <summary>
        /// 就诊登记号
        /// </summary>
        public string aaz217 { get; set; }
        /// <summary>
        /// 人员医疗结算事件id(就诊结算id)
        /// </summary>
        public string aaz216 { get; set; }
        /// <summary>
        /// 清算类别
        /// </summary>
        public string bka015 { get; set; }
        /// <summary>
        /// 参保地统筹区编号
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 参保地分中心编码
        /// </summary>
        public string baa009 { get; set; }
        /// <summary>
        /// 就医地统筹区编号
        /// </summary>
        public string baa010 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// 参保身份
        /// </summary>
        public string aac066 { get; set; }
        /// <summary>
        /// 特殊人员类别
        /// </summary>
        public string bkc113 { get; set; }
       
        /// <summary>
        /// 费用结算时间
        /// </summary>
        public string bkc060 { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public string akc264 { get; set; }
        /// <summary>
        /// 全自费部分
        /// </summary>
        public string akc253 { get; set; }
        /// <summary>
        /// 先自付部分
        /// </summary>
        public string akc228 { get; set; }
        /// <summary>
        /// 超限自付部分
        /// </summary>
        public string akc268 { get; set; }
        /// <summary>
        /// 进入报销范围部分
        /// </summary>
        public string bkc042 { get; set; }
        /// <summary>
        /// 本次起付线
        /// </summary>
        public string bke002 { get; set; }
        /// <summary>
        /// 本年累积起付线
        /// </summary>
        public string bke003 { get; set; }
        /// <summary>
        /// 个人账户支付
        /// </summary>
        public string akb066 { get; set; }
        /// <summary>
        /// 医保统筹支付合计（不包含个人账户支付）
        /// </summary>
        public string akb068 { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public string akb067 { get; set; }
        /// <summary>
        /// 本次个人账户最多可支付金额(个人账户支付)
        /// </summary>
        public string bkc143 { get; set; }
        /// <summary>
        /// 城镇职工基本保险
        /// </summary>
        public string ake039 { get; set; }
        /// <summary>
        /// 公务员医疗保险
        /// </summary>
        public string ake035 { get; set; }
        /// <summary>
        /// 城镇职工补充保险
        /// </summary>
        public string ake029 { get; set; }
        /// <summary>
        /// 生育保险
        /// </summary>
        public string ame001 { get; set; }
        /// <summary>
        /// 居民门诊统筹
        /// </summary>
        public string ake039_jm { get; set; }
        /// <summary>
        /// 城乡居民门大病险
        /// </summary>
        public string bkc010 { get; set; }
        /// <summary>
        /// 新农合
        /// </summary>
        public string ake039_xnh { get; set; }
        /// <summary>
        /// 其它险种
        /// </summary>
        public string ake039_qt { get; set; }
        /// <summary>
        /// 报销情况说明
        /// </summary>
        public string bke031 { get; set; }
        /// <summary>
        /// 医保结算时间
        /// </summary>
        public string aae036 { get; set; }
        public OutpatientSimulatedSettlementDataRow DataRow { get; set; }

}
    public  class OutpatientSimulatedSettlementDataRow
    {
        public List<OutpatientSimulatedSettlementRow> Row { get; set; }
    }
    public class OutpatientSimulatedSettlementRow
    {
        
        /// <summary>
        /// 费用明细序号
        /// </summary>
        public string bke019 { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public string akc264 { get; set; }
        /// <summary>
        /// 定价上限金额
        /// </summary>
        public string aka068 { get; set; }
        /// <summary>
        /// 自费比例
        /// </summary>
        public string aka057 { get; set; }
        /// <summary>
        /// 全自费部分
        /// </summary>
        public string akc253 { get; set; }
        /// <summary>
        /// 先自付部分
        /// </summary>
        public string akc228 { get; set; }
        /// <summary>
        /// 超限自付部分
        /// </summary>
        public string akc268 { get; set; }
        /// <summary>
        /// 进入报销范围部分
        /// </summary>
        public string bkc042 { get; set; }
        /// <summary>
        /// 收费项目等级
        /// </summary>
        public string aka065 { get; set; }

    }
}
