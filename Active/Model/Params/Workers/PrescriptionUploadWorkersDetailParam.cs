using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Workers
{
    public class PrescriptionUploadWorkersDetailListParam
    {
        public List<PrescriptionUploadWorkersDetailParam> ROW { get; set; }
    }

    public  class PrescriptionUploadWorkersDetailParam
    {/// <summary>
     /// 服务机构号
     /// </summary>
        public string AKB020 { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        public string AKC220 { get; set; }
        /// <summary>
        /// 费用顺序号（一个入院号的唯一值）
        /// </summary>
        public Int32 AKC584 { get; set; }
        /// <summary>
        /// 收费项目编码
        /// </summary>
        public string AKC222 { get; set; }
        /// <summary>
        /// 医院内码
        /// </summary>
        public string AKC515 { get; set; }
        /// <summary>
        /// 处方日期
        /// </summary>
        public string AKC221 { get; set; }
        /// <summary>
        /// 结算日期
        /// </summary>
        public string AAE040 { get; set; }
        /// <summary>
        /// 收费类别
        /// </summary>
        public string AKA063 { get; set; }
        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string AKC223 { get; set; }
        /// <summary>
        /// 收费项目等级
        /// </summary>
        public string AKA065 { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double AKC225 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public double AKC226 { get; set; }
        /// <summary>
        /// 金额（每条费用明细的数据校验为传入的金额（四舍五入到两位小数）和传入的单价*传入的数量（四舍五入到两位小数）必须相等，检查不等的会提示报错）
        /// </summary>
        public double AKC227 { get; set; }
        /// <summary>
        /// 自付金额
        /// </summary>
        public double AKC228 { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string AKA070 { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        public double AKA071 { get; set; }
        /// <summary>
        /// AKA072
        /// </summary>
        public string AKA072 { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        public string AKA073 { get; set; }
        /// <summary>
        /// 规格名称
        /// </summary>
        public string AKA074 { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string AKA067 { get; set; }
        /// <summary>
        /// 执行天数
        /// </summary>
        public int AKC229 { get; set; }
        /// <summary>
        /// 备注 （如果有冲减必须写明冲减原因）
        /// </summary>
        public string AAE013 { get; set; }
        /// <summary>
        /// 医师工号
        /// </summary>
        public string CKC691 { get; set; }
        /// <summary>
        /// 限制药品审批标志  （0未审核1审核通过2审核不通过）
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
