using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 
/// </summary>
   public class InformationJsonDto
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
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "备注")]
        public string Remark { get; set; }
   
    }
}
