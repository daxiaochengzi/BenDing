using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto
{/// <summary>
 /// 特殊疾病报销查询
 /// </summary>
    public class IdentificationSpecialReimbursementQueryDto : IniDto
    {
        public PO_JSXX PO_JSXX { get; set; }
      
    }
    public class PO_JSXX
    {
        public IdentificationSpecialCancelQueryRowDto ROW { get; set; }
    }
    
    public class IdentificationSpecialCancelQueryRowDto
    {/// <summary>
     /// 个人编号
     /// </summary>
        public string PO_AAC001 { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string PO_AAC002 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PO_AAC003 { get; set; }
        /// <summary>
        /// 报销流水号
        /// </summary>
        public string PO_AKC604 { get; set; }
        /// <summary>
        /// 医疗机构编码
        /// </summary>
        public string PO_AAZ107 { get; set; }
        /// <summary>
        /// 医疗机构名称
        /// </summary>
        public string PO_CKB519 { get; set; }
        /// <summary>
        /// 医疗机构就诊流水号
        /// </summary>
        public string PO_AKC190 { get; set; }
        /// <summary>
        /// 就诊特殊疾病1
        /// </summary>
        public string PO_CKZ735_1 { get; set; }
        /// <summary>
        /// 就诊特殊疾病2
        /// </summary>
        public string PO_CKZ735_2 { get; set; }
        /// <summary>
        /// 就诊特殊疾病3
        /// </summary>
        public string PO_CKZ735_3 { get; set; }
        /// <summary>
        /// 就诊特殊疾病4
        /// </summary>
        public string PO_CKZ735_4 { get; set; }
        /// <summary>
        /// 就诊特殊疾病5
        /// </summary>
        public string PO_CKZ735_5 { get; set; }
        /// <summary>
        /// 消费时间
        /// </summary>
        public string PI_AKE010 { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string PO_AAE011 { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public string PO_CKC501 { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        public string PO_CKC511 { get; set; }
        /// <summary>
        /// 进入统筹费用
        /// </summary>
        public string PO_CKC705 { get; set; }
        /// <summary>
        /// 统筹支付比例
        /// </summary>
        public string PO_CKA523 { get; set; }
        /// <summary>
        /// 超封顶线金额
        /// </summary>
        public string PO_CKC509 { get; set; }
        /// <summary>
        /// 医保统筹支付金额
        /// </summary>
        public string PO_AKB066 { get; set; }
        /// <summary>
        /// 汇总标志
        /// </summary>
        public string PO_CKE883 { get; set; }
        /// <summary>
        /// 汇总流水号
        /// </summary>
        public string PO_BAE007 { get; set; }
        /// <summary>
        /// 超限价金额
        /// </summary>
        public string PO_CKE84 { get; set; }
    }
    /// <summary>
    /// list
    /// </summary>
    public class IdentificationSpecialReimbursementQueryListDto : IniDto
    {
        public PO_JSXXList PO_JSXX { get; set; }

    }
    public class PO_JSXXList
    {
        public List<IdentificationSpecialCancelQueryRowDto>  ROW { get; set; }
    }
}
