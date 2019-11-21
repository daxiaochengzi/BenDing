using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Resident
{/// <summary>
/// 处方上传回参
/// </summary>
   public class RetrunPrescriptionUploadDto
    { /// <summary>
    /// 成功条数
    /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 合计
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
    }
}
