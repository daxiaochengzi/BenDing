using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Base
{/// <summary>
/// 查询日期公用实体
/// </summary>
   public class QueryDateBaseDto
        {/// <summary>
        /// 开始日期
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public  string EndTime { get; set; }

    }
}
