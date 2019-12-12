using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Resident
{
  public  class QueryMedicalInsurancePairCodeDto
    {  /// <summary>
    /// 
    /// </summary>
        public string FixedEncoding { get; set; }
        /// <summary>
        /// his项目编码
        /// </summary>
        public string DirectoryCode { get; set; }
        /// <summary>
        /// his项目类别编码
        /// </summary>
        public string DirectoryCategoryCode { get; set; }
        /// <summary>
        /// 医保编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 医保类型
        /// </summary>
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 医保项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 医保项目等级
        /// </summary>
        public string ProjectLevel { get; set; }
        /// <summary>
        /// 医保剂型
        /// </summary>
        public string Formulation { get; set; }
        /// <summary>
        /// 医保规格
        /// </summary>
        public string Specification { get; set; }
        /// <summary>
        /// 医保单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 二级乙等以下
        /// </summary>
        public decimal ZeroBlock { get; set; }
        /// <summary>
        /// 二级乙等
        /// </summary>
        public decimal OneBlock { get; set; }
        /// <summary>
        /// 二级甲等
        /// </summary>
        public decimal TwoBlock { get; set; }
        /// <summary>
        /// 三级乙等
        /// </summary>
        public decimal ThreeBlock { get; set; }
        /// <summary>
        /// 三级甲等
        /// </summary>
        public decimal FourBlock { get; set; }
        /// <summary>
        /// 职工医保自付比例
        /// </summary>
       
        public decimal WorkersSelfPayProportion { get; set; }
        /// <summary>
        /// 居民医保自付比例
        /// </summary>
      
        public decimal ResidentSelfPayProportion { get; set; }
       
        /// <summary>
        /// 居民普通门诊报销标志
        /// </summary>

        public string ResidentOutpatientSign { get; set; }
    
        /// <summary>
        /// 限制用药标志
        /// </summary>

        public string RestrictionSign { get; set; }
        /// <summary>
        /// 居民普通门诊报销限价
        /// </summary>
        public decimal ResidentOutpatientBlock { get; set; }
        /// <summary>
        /// 限制支付范围
        /// </summary>
      
        public string LimitPaymentScope { get; set; }
        /// <summary>
        /// 生产厂家
        /// </summary>

        public string Manufacturer { get; set; }
        
    }
}
