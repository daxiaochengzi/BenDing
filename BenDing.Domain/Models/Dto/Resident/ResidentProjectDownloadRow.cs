using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Resident
{
   public class ResidentProjectDownloadRow
    {
        public Guid Id { get; set; }

        /// <summary>
     /// 收费项目编码
     /// </summary>

        public string ProjectCode { get; set; }
        /// <summary>
        /// 收费项目名称
        /// </summary>
      
        public string ProjectName { get; set; }
        /// <summary>
        /// 分类代码
        /// </summary>
       
        public string ProjectCodeType { get; set; }
        ///// <summary>
        ///// 收费项目等级
        ///// </summary>  
        //public string ProjectLevel { get; set; }
        ///// <summary>
        ///// 职工医保自付比例
        ///// </summary>
     
        //public string WorkersSelfPayProportion { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
     
        public string Unit { get; set; }
        /// <summary>
        /// 拼音助记码
        /// </summary>
      
        public string MnemonicCode { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
      
        public string Formulation { get; set; }
        ///// <summary>
        ///// 居民医保自付比例
        ///// </summary>
      
        //public string ResidentSelfPayProportion { get; set; }

        /// <summary>
        /// 限制用药标志
        /// </summary>
      
       public string RestrictionSign { get; set; }
        ///// <summary>
        ///// 零档限价（二级乙等以下）
        ///// </summary>
       
        //public string ZeroBlock { get; set; }
        ///// <summary>
        ///// 一档限价（二级乙等）
        ///// </summary>
       
        //public string OneBlock { get; set; }
        ///// <summary>
        ///// 二档限价（二级甲等）
        ///// </summary>
     
        //public string TwoBlock { get; set; }
        ///// <summary>
        ///// 三档限价（三级乙等）
        ///// </summary>
      
        //public string ThreeBlock { get; set; }

        ///// <summary>
        ///// 四档限价（三级甲等）
        ///// </summary>
       
        //public string FourBlock { get; set; }
        ///// <summary>
        ///// 有效标志
        ///// </summary>
      
        //public string EffectiveSign { get; set; }
        ///// <summary>
        ///// 居民普通门诊报销标志
        ///// </summary>
       
        //public string ResidentOutpatientSign { get; set; }
        ///// <summary>
        ///// 居民普通门诊报销限价
        ///// </summary>
        
        //public string ResidentOutpatientBlock { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>
      
        public string Manufacturer { get; set; }
        /// <summary>
        /// 药品准字号
        /// </summary>
      
        public string QuasiFontSize { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
       
        public string Specification { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
       
        public string Remark { get; set; }
        /// <summary>
        /// 新码标志
        /// </summary>
     
        public string NewCodeMark { get; set; }
        /// <summary>
        /// 最近一次变更日期
        /// </summary>
      
        public DateTime NewUpdateTime { get; set; }
        ///// <summary>
        ///// 开始时间
        ///// </summary>
      
        //public string StartTime { get; set; }
        ///// <summary>
        ///// 终止时间
        ///// </summary>
      
        //public string EndTime { get; set; }

        /// <summary>
        /// 限制支付范围
        /// </summary>
        
        public string LimitPaymentScope { get; set; }
    }
}
