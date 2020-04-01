using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.OutpatientDepartment;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Web;

namespace BenDing.Service.Interfaces
{
  public  interface IOutpatientDepartmentService
    {/// <summary>
     /// 门诊结算
     /// </summary>
     /// <param name="param"></param>
     /// <returns></returns>
        OutpatientDepartmentCostInputDto OutpatientDepartmentCostInput(GetOutpatientPersonParam param);

        /// <summary>
        /// 取消门诊结算
        /// </summary>
        /// <param name="param"></param>
        void CancelOutpatientDepartmentCost(CancelOutpatientDepartmentCostUiParam param);
        /// <summary>
        /// 门诊病人结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        QueryOutpatientDepartmentCostjsonDto QueryOutpatientDepartmentCost(UiBaseDataParam param);
        //门诊月结
        void MonthlyHospitalization(MonthlyHospitalizationUiParam param);
        //门诊月结取消
        void CancelMonthlyHospitalization(CancelMonthlyHospitalizationUiParam param);
        /// <summary>
        /// 门诊计划生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
         WorkerHospitalizationPreSettlementDto OutpatientPlanBirthPreSettlement(
            OutpatientPlanBirthPreSettlementUiParam param);
        /// <summary>
        /// 门诊计划生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto OutpatientPlanBirthSettlement(
            OutpatientPlanBirthSettlementUiParam param);
        /// <summary>
        /// 获取计划生育结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        OutpatientPlanBirthSettlementParam GetOutpatientPlanBirthSettlementParam(
            OutpatientPlanBirthSettlementUiParam param
        );
    }
}
