using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Resident
{
  public  class QueryMedicalInsurancePairCodeDto
    {  
        public string FixedEncoding { get; set; }
        /// <summary>
        /// his项目编码
        /// </summary>
        public string DirectoryCode { get; set; }
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
    }
}
