using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Params.Workers;

namespace BenDing.Service.Interfaces
{
   public interface IWorkerMedicalInsuranceService
   { /// <summary>
       /// 职工入院登记
       /// </summary>
       /// <param name="param"></param>
       /// <returns></returns>
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
        /// 职工划卡
        /// </summary>
        /// <param name="param"></param>
        void WorkerStrokeCard(WorkerStrokeCardParam param);
   }
}
