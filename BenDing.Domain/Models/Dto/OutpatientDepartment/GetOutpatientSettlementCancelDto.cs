using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.OutpatientDepartment
{
   public class GetOutpatientSettlementCancelDto
    {/// <summary>
        /// 结算编号
        /// </summary>
        public string SettlementNo { get; set; }
        /// <summary>
        ///取消经办人
        /// </summary>
       
        public string CancelOperator { get; set; }
    }
}
