using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 三大目录固定编码表
/// </summary>
   public class HospitalThreeCatalogueCodeDto
    {/// <summary>
    /// 目录编码
    /// </summary>
        public string DirectoryCode { get; set; }
        /// <summary>
        /// 固定编码
        /// </summary>
        public string FixedEncoding { get; set; }
    }
}
