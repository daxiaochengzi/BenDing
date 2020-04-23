using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Workers;
using BenDing.Domain.Models.Params.Base;
using BenDing.Domain.Models.Params.UI;
using BenDing.Domain.Models.Params.Workers;

namespace BenDing.Service.Interfaces
{
   public interface IWorkerMedicalInsuranceService
   {
       /// <summary>
       /// 职工入院登记
       /// </summary>
       /// <param name="param"></param>
       /// <returns></returns>
       WorkerHospitalizationRegisterDto WorkerHospitalizationRegister(WorKerHospitalizationRegisterUiParam param);

        /// <summary>
        /// 职工入院登记修改
        /// </summary>
        /// <param name="uiParam"></param>
        void ModifyWorkerHospitalization(HospitalizationModifyUiParam uiParam);
        /// <summary>
        /// 职工住院预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerHospitalizationPreSettlement(UiBaseDataParam param);
        /// <summary>
        /// 职工住院结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerHospitalizationSettlement(WorkerHospitalizationSettlementUiParam param);
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
           BirthHospitalizationRegisterUiParam param);
        /// <summary>
        /// 职工预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerBirthPreSettlement(WorkerBirthPreSettlementUiParam param);
        /// <summary>
        /// 职工生育结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerBirthSettlement(WorkerBirthSettlementUiParam param);
   }
}
