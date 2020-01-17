using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Service.Interfaces
{
  public  interface IOutpatientDepartmentService
    {/// <summary>
     /// 门诊结算
     /// </summary>
     /// <param name="param"></param>
     /// <returns></returns>
        OutpatientDepartmentCostInputJsonDto OutpatientDepartmentCostInput(GetOutpatientPersonParam param);

        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <param name="param"></param>
        void CancelOutpatientDepartmentCost(UiBaseDataParam param);
        /// <summary>
        /// 门诊病人结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        QueryOutpatientDepartmentCostjsonDto QueryOutpatientDepartmentCost(UiBaseDataParam param);
    }
}
