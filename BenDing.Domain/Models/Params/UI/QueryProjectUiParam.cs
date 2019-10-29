using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Base;

namespace BenDing.Domain.Models.Params.UI
{
   public class QueryProjectUiParam: PaginationDto
    {/// <summary>
     /// 项目编码
     /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>

        public string ProjectName { get; set; }
        /// <summary>
        /// 项目编码类别
        /// </summary>
        public string ProjectCodeType { get; set; }
    }
}
