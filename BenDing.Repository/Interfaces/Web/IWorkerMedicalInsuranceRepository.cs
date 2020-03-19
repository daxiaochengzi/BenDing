using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Params.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Repository.Interfaces.Web
{
    public interface IWorkerMedicalInsuranceRepository
    {/// <summary>
     /// 职工入院登记
     /// </summary>
     /// <param name="param"></param>
        WorkerHospitalizationRegisterDto WorkerHospitalizationRegister(WorKerHospitalizationRegisterParam param);

        /// <summary>
        /// 职工入院登记修改
        /// </summary>
        /// <param name="param"></param>
        void ModifyWorkerHospitalization(ModifyWorkerHospitalizationParam param);
        /// <summary>
        /// 职工住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerHospitalizationPreSettlement(WorkerHospitalizationPreSettlementParam param);
        /// <summary>
        /// 职工住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerHospitalizationSettlement(WorkerHospitalizationSettlementParam param);
      
        /// <summary>
        /// 职工住院结算取消
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string WorkerSettlementCancel(WorkerSettlementCancelParam param);
        /// <summary>
        /// 职工划卡取消
        /// </summary>
        /// <param name="param"></param>
        void CancelWorkerStrokeCard(CancelWorkersStrokeCardParam param);

        /// <summary>
        /// 职工结算查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        QueryWorkerHospitalizationSettlementDto QueryWorkerHospitalizationSettlement(
            QueryWorkerHospitalizationSettlementParam param);
        /// <summary>
        /// 职工生育入院登记
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerBirthHospitalizationRegisterDto WorkerBirthHospitalizationRegister(
            WorkerBirthHospitalizationRegisterParam param);
        /// <summary>
        /// 职工生育预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerBirthPreSettlement(WorkerBirthPreSettlementParam param);
        /// <summary>
        /// 职工生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
         WorkerHospitalizationPreSettlementDto WorkerBirthSettlement(WorkerBirthSettlementParam param);
    }
}
