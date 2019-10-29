using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Base;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
   public class QueryCatalogDto
        {/// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
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
        /// 备注
        /// </summary>
       
        public string Remark { get; set; }
        ///// <summary>
        ///// 创建时间
        ///// </summary>
      
       public DateTime DirectoryCreateTime { get; set; }
    }
}
