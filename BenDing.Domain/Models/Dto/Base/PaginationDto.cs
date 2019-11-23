using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Base
{/// <summary>
/// 
/// </summary>
   public class PaginationDto
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }
    }
}
