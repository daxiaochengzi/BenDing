using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Single
{
    /// <summary>
    /// 单病种预结算查询
    /// </summary>
    public class SinglePreSettlementQueryDto:IniDto
    { /// <summary>
      /// 身份证号  
      /// </summary>
        public string PO_AAC002 { get; set; }
        /// <summary>
        /// 人员编号  
        /// </summary>
        public string PO_AAC001 { get; set; }
        /// <summary>
        /// 姓名  
        /// </summary>
        public string PO_AAC003 { get; set; }
        /// <summary>
        /// 就诊类别  
        /// </summary>
        public string PO_AKA130 { get; set; }
        /// <summary>
        /// 就诊医院  
        /// </summary>
        public string PO_CKB519 { get; set; }
        /// <summary>
        /// 入院日期  
        /// </summary>
        public string PO_CKC537 { get; set; }
        /// <summary>
        /// 出院日期  
        /// </summary>
        public string PO_CKC538 { get; set; }
        /// <summary>
        /// 入院诊断疾病名称  
        /// </summary>
        public string PO_CKC540 { get; set; }
        /// <summary>
        /// 出院诊断疾病名称  
        /// </summary>
        public string PO_CKC546 { get; set; }
        /// <summary>
        /// 计算前谈判药限额  
        /// </summary>
        public string PO_JSQ_TPYXE { get; set; }
        /// <summary>
        /// 计算后谈判药限额  
        /// </summary>
        public string PO_JSH_TPYXE { get; set; }
        /// <summary>
        /// 发生费用金额  
        /// </summary>
        public string PO_FYZE { get; set; }
        /// <summary>
        /// 基本统筹支付  
        /// </summary>
        public string PO_TCZF { get; set; }
        /// <summary>
        /// 生育补助  
        /// </summary>
        public string PO_SYBZ { get; set; }
        /// <summary>
        /// 居民大病保险支付金额  
        /// </summary>
        public string PO_DBZF { get; set; }
        /// <summary>
        /// 职工补充医疗支付金额  
        /// </summary>
        public string PO_BCZF { get; set; }
        /// <summary>
        /// 职工公务员补助支付金额  
        /// </summary>
        public string PO_GWYBZ { get; set; }
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
        /// 单病种限价标准  
        /// </summary>
        public string PO_DBZXJBZ { get; set; }
        /// <summary>
        /// 超单病种限价标准医院承担费用  
        /// </summary>
        public string PO_DBZYYZF { get; set; }
        /// <summary>
        /// 未达单病种限价标准补给医院差额  
        /// </summary>
        public string PO_DBZYYCE { get; set; }
        /// <summary>
        /// 账户支付  
        /// </summary>
        public string PO_ZHZF { get; set; }
        /// <summary>
        /// 患者支付金额  
        /// </summary>
        public string PO_XJZF { get; set; }
        /// <summary>
        /// 起付金额  
        /// </summary>
        public string PO_QFJE { get; set; }
        /// <summary>
        /// 备注  
        /// </summary>
        public string PO_BZ { get; set; }
       
    }
}
