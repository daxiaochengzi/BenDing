using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Base;

namespace BenDing.Domain.Models.Params.Web
{
   public class QueryCatalogUiParam
    {/// <summary>
     /// 目录编码
     /// </summary>
        public string DirectoryCode { get; set; }
        /// <summary>
        /// 目录名称
        /// </summary>

        public string DirectoryName { get; set; }
        /// <summary>
        /// 目录类别
        /// </summary>
        public string DirectoryCategoryCode { get; set; }

        public  int Limit { get; set; }

        public int  Page { get; set; }

    }
}
