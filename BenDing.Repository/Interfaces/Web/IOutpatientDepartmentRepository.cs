using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.JsonEntity;
using BenDing.Domain.Models.Dto.OutpatientDepartment;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Params.OutpatientDepartment;

namespace BenDing.Repository.Interfaces.Web
{
  public  interface IOutpatientDepartmentRepository
      {/// <summary>
       /// 门诊费用录入
       /// </summary>
       /// <param name="param"></param>
       /// <returns></returns>
          OutpatientDepartmentCostInputDto OutpatientDepartmentCostInput(OutpatientDepartmentCostInputParam param);
        /// <summary>
        /// 门诊费取消
        /// </summary>
        /// <param name="param"></param>
        void CancelOutpatientDepartmentCost(CancelOutpatientDepartmentCostParam param);
        /// <summary>
        /// 门诊费用查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
       QueryOutpatientDepartmentCostjsonDto QueryOutpatientDepartmentCost(
              QueryOutpatientDepartmentCostParam param);
        /// <summary>
        /// 门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        MonthlyHospitalizationDto MonthlyHospitalization(MonthlyHospitalizationParam param);
        /// <summary>
        /// 取消门诊月结汇总
        /// </summary>
        /// <param name="param"></param>
        void CancelMonthlyHospitalization(CancelMonthlyHospitalizationParam param);
        /// <summary>
        /// 门诊计划生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
          WorkerHospitalizationPreSettlementDto OutpatientPlanBirthPreSettlement(
              OutpatientPlanBirthPreSettlementParam param);
        /// <summary>
        /// 门诊计划生育结算
        ///  </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto OutpatientPlanBirthSettlement(
              OutpatientPlanBirthSettlementParam param);
        /// <summary>
        /// 门诊计划生育结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto OutpatientPlanBirthSettlementQuery(
              OutpatientPlanBirthSettlementQueryParam param);
        /// <summary>
        /// 计划生育取消结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
          void OutpatientPlanBirthSettlementCancel(
              OutpatientPlanBirthSettlementCancelParam param);

      }
}
