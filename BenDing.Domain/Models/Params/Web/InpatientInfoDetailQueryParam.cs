using System;
using System.Collections.Generic;
using System.Text;

namespace BenDing.Domain.Models.Params.Web
{
   public class InpatientInfoDetailQueryParam
    {/// <summary>
    /// 明细id
    /// </summary>
        public List<string> IdList { get; set; } 
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 是否上传
        /// </summary>
        public int? UploadMark { get; set; }
    }
}
