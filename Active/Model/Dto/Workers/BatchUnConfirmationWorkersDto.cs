using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Workers
{/// <summary>
    /// 费用批次未注册确认信息查询  
    /// </summary>
    public class BatchUnConfirmationWorkersDto
    {  /// <summary>
        /// 批次号(查询结果) 以逗号为分隔符分隔的未确认批次号字符串  
        /// </summary>
        public string Po_pch { get; set; }
    }
}
