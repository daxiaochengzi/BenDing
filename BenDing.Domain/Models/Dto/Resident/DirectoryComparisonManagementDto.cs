using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Resident
{
  public  class DirectoryComparisonManagementDto
    {  /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// his固定编码
        /// </summary>
        public string FixedEncoding { get; set; }
        /// <summary>
        /// 目录编码
        /// </summary>

        public string DirectoryCode { get; set; }
        /// <summary>
        /// 目录名称
        /// </summary>

        public string DirectoryName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>

        public string MnemonicCode { get; set; }
        /// <summary>
        /// 目录类别编码
        /// </summary>
        public string DirectoryCategoryCode { get; set; }
        /// <summary>
        /// 目录类别名称
        /// </summary>

        public string DirectoryCategoryName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>

        public string Unit { get; set; }
        /// <summary>
        /// 规格
        /// </summary>

        public string Specification { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>

        public string Formulation { get; set; }
        /// <summary>
        /// 生产厂家名称
        /// </summary>

        public string ManufacturerName { get; set; }
       
        /// <summary>
        /// 医保项目编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 医保项目编码
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 医保备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        ///  医保限制使用范围
        /// </summary>
        public string LimitPaymentScope  {get; set; }
        /// <summary>
        /// 医保准字号
        /// </summary>
        public string QuasiFontSize { get; set; }
        /// <summary>
        /// 医保类别
        /// </summary>
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 医保报销比例
        /// </summary>
        public string ProjectLevel { get; set; }
        /// <summary>
        /// 对码时间
        /// </summary>
        public DateTime? PairCodeTime { get; set; }
        /// <summary>
        /// 对码操作人员
        /// </summary>
        public  string PairCodeUser { get; set; }


    }
}
