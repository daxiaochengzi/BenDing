using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Workers;

namespace BenDing.Service.Interfaces
{
    public interface IWorkerMedicalInsuranceNewService
    {/// <summary>
     /// 获取职工入院登记参数
     /// </summary>
     /// <param name="param"></param>
     /// <returns></returns>
        WorKerHospitalizationRegisterParam GetWorkerHospitalizationRegisterParam(
          WorKerHospitalizationRegisterUiParam param);
        /// <summary>
        /// 职工入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationRegisterDto WorkerHospitalizationRegister(WorKerHospitalizationRegisterUiParam param);
        /// <summary>
        /// 获取职工入院登记修改参数
        /// </summary>
        /// <param name="uiParam"></param>
        /// <returns></returns>
        ModifyWorkerHospitalizationParam GetModifyWorkerHospitalizationParam(HospitalizationModifyUiParam uiParam);
        /// <summary>
        /// 职工入院登记修改
        /// </summary>
        /// <param name="uiParam"></param>
        void ModifyWorkerHospitalization(HospitalizationModifyUiParam uiParam);
        /// <summary>
        /// 职工住院预结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementParam GetWorkerHospitalizationPreSettlementParam(
          HospitalizationPreSettlementUiParam param);
        /// <summary>
        /// 职工住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerHospitalizationPreSettlement(
          HospitalizationPreSettlementUiParam param);
        /// <summary>
        /// 职工住院预结算参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationSettlementParam GetWorkerHospitalizationSettlementParam(
          WorkerHospitalizationSettlementUiParam param);

        /// <summary>
        /// 职工住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerHospitalizationSettlement(
            WorkerHospitalizationSettlementUiParam param);
        /// <summary>
        /// 职工取消结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string WorkerSettlementCancel(WorkerSettlementCancelParam param);

        /// <summary>
        /// 获取职工生育入院登记参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerBirthHospitalizationRegisterParam GetWorkerBirthHospitalizationRegisterParam(
          BirthHospitalizationRegisterUiParam param);

        /// <summary>
        /// 职工生育入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerBirthHospitalizationRegisterDto WorkerBirthHospitalizationRegister(
          BirthHospitalizationRegisterUiParam param);
        /// <summary>
        /// 获取职工计划生育预结算入参
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerBirthPreSettlementParam GetWorkerBirthPreSettlementParam(WorkerBirthPreSettlementUiParam param);
        /// <summary>
        /// 职工生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerBirthPreSettlement(WorkerBirthPreSettlementUiParam param);
        /// <summary>
        /// 获取职工生育结算入参
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerBirthSettlementParam GetWorkerBirthSettlementParam(WorkerBirthSettlementUiParam param);
        /// <summary>
        /// 职工生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerBirthSettlement(WorkerBirthSettlementUiParam param);

    }
}
