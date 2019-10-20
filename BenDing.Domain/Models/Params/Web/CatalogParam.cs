using System;
using System.Collections.Generic;
using System.Text;
using BenDing.Domain.Models.Enums;


namespace BenDing.Domain.Models.Params.Web
{
   public class CatalogParam
    {/// <summary>
    /// 验证码
    /// </summary>
        public string 验证码 { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public  string 机构编码 { get; set; }
        /// <summary>
        /// 目录类型
        /// </summary>
        public CatalogTypeEnum CatalogType { get; set; }
        /// <summary>
        /// 条数
        /// </summary>
        public   Int32 条数 { get; set; }
    }
}
