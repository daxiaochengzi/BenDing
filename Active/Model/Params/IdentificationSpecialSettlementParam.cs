using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
 /// 特殊疾病预结算
 /// </summary>
    
    public class IdentificationSpecialSettlementParam
    {
        /// <summary>
        /// 身份标志
        /// </summary>
        public string PI_SFBZ { get; set; }
        /// <summary>
        /// 传入标志
        /// </summary>
        public string PI_CRBZ { get; set; }
        /// <summary>
        /// 医疗机构就诊流水号
        /// </summary>
        public string PI_AKC190 { get; set; }
        /// <summary>
        /// 就诊特殊疾病1
        /// </summary>
        public string PI_CKZ735_1 { get; set; }
        /// <summary>
        /// 就诊特殊疾病2
        /// </summary>
        public string PI_CKZ735_2 { get; set; }
        /// <summary>
        /// 就诊特殊疾病3
        /// </summary>
        public string PI_CKZ735_3 { get; set; }
        /// <summary>
        /// 就诊特殊疾病4
        /// </summary>
        public string PI_CKZ735_4 { get; set; }
        /// <summary>
        /// 就诊特殊疾病5
        /// </summary>
        public string PI_CKZ735_5 { get; set; }
        /// <summary>
        /// 消费时间
        /// </summary>
        public string PI_AKE010 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string PI_NUM { get; set; }
        /// <summary>
        /// 明细
        /// </summary>
        public List<IdentificationSpecialSettlementPI_FYMXParam>   PI_FYMX { get; set; }
        /// <summary>
        /// 金额合计
        /// </summary>
        public string PI_CKC501 { get; set; }
    }
    public class IdentificationSpecialSettlementPI_FYMXParam
    {/// <summary>
     /// 费用序号
     /// </summary>
        public string BKE019 { get; set; }
        /// <summary>
        /// 社保收费项目编码
        /// </summary>
        public string AKE001 { get; set; }
        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string AKE002 { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string CKE521 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string AKC226 { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public string CKC526 { get; set; }
        /// <summary>
        /// 医院内规格
        /// </summary>
        public string AKA074 { get; set; }
        /// <summary>
        /// 医院内剂型
        /// </summary>
        public string CKA588 { get; set; }
        /// <summary>
        /// 每次用量
        /// </summary>
        public string BKC044 { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        public string BKC045 { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string BKC046 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string AAE013 { get; set; }
        /// <summary>
        /// 医师工号
        /// </summary>
        public string CKC691 { get; set; }
        /// <summary>
        /// 限制药品审批标志
        /// </summary>
        public string CKE841 { get; set; }
        /// <summary>
        /// 限制药品审批人
        /// </summary>
        public string CKE843 { get; set; }
        /// <summary>
        /// 限制药品审批日期
        /// </summary>
        public string CKE844 { get; set; }
        /// <summary>
        /// 限制药品审批意见
        /// </summary>
        public string AKC586 { get; set; }
       
    }
}
