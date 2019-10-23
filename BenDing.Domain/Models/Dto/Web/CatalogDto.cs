using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 三大目录
/// </summary>
   public class CatalogDto
    {/// <summary>
     /// 目录编码
     /// </summary>
        [JsonProperty(PropertyName = "目录编码")]
        public string DirectoryCode { get; set; }
        /// <summary>
        /// 目录名称
        /// </summary>
        [JsonProperty(PropertyName = "目录名称")]
        public string DirectoryName { get; set; }
        /// <summary>
        /// 助记码
        /// </summary>
        [JsonProperty(PropertyName = "助记码")]
        public string MnemonicCode { get; set; }
        /// <summary>
        /// 目录类别名称
        /// </summary>
        [JsonProperty(PropertyName = "目录类别名称")]
        public string DirectoryCategoryName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [JsonProperty(PropertyName = "单位")]
        public string Unit { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [JsonProperty(PropertyName = "规格")]
        public string Specification { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        [JsonProperty(PropertyName = "剂型")]
        public string Formulation { get; set; }
        /// <summary>
        /// 生产厂家名称
        /// </summary>
        [JsonProperty(PropertyName = "生产厂家名称")]
        public string ManufacturerName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty(PropertyName = "创建时间")]
        public string DirectoryCreateTime { get; set; }
      
    }
}
