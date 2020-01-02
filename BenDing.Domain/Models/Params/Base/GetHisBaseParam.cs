using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Base
{/// <summary>
/// 获取his基本参数
/// </summary>
   public class GetHisBaseParam
    {/// <summary>
    /// 业务id
    /// </summary>
        public string BId { get; set; }
        /// <summary>
        /// 操作人员id
        /// </summary>
        public string EmpId { get; set; }
        /// <summary>
        /// 医保交易码
        /// </summary>
        public string TransKey { get; set; }

    }
}
