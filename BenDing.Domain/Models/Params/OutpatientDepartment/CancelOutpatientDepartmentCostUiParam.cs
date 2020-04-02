using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{
   public class CancelOutpatientDepartmentCostUiParam:UiBaseDataParam
    {/// <summary>
    /// 取消结算备注
    /// </summary>
        public string CancelSettlementRemarks { get; set; }
        
    }
}
