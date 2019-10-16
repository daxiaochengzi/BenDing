using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params
{/// <summary>
 /// 特殊疾病报销
 /// </summary>
    public class IdentificationSpecialReimbursementParam
    {
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
        public string PI_ckz735_1 { get; set; }
        /// <summary>
        /// 就诊特殊疾病2
        /// </summary>
        public string PI_ckz735_2 { get; set; }
        /// <summary>
        /// 就诊特殊疾病3
        /// </summary>
        public string PI_ckz735_3 { get; set; }
        /// <summary>
        /// 就诊特殊疾病4
        /// </summary>
        public string PI_ckz735_4 { get; set; }
        /// <summary>
        /// 就诊特殊疾病5
        /// </summary>
        public string PI_ckz735_5 { get; set; }
        /// <summary>
        /// 消费时间
        /// </summary>
        public string PI_AKE010 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string PI_NUM { get; set; }
        /// <summary>
        /// 金额合计
        /// </summary>
        public string PI_CKC501 { get; set; }
        /// <summary>
        /// 个人账户拟下账金额
        /// </summary>
        public string PI_BKC142 { get; set; }
        /// <summary>
        /// 读卡器端口
        /// </summary>
        public string PI_ICK_READERPORT { get; set; }
        /// <summary>
        /// 社保卡密码
        /// </summary>
        public string PI_ICK_CARDPWD { get; set; }

    }
}
