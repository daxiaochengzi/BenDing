using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Resident
{
   public class AddProjectBatchParam
    {/// <summary>
    /// 病人入院登记id
    /// </summary>
        public string BusinessId { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public  string BatchNumber { get; set; }
       
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        public Guid Id { get; set; }
    }
}
