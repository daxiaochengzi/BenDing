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
        //职工入院登记修改
        void ModifyWorkerHospitalization(ModifyWorkerHospitalizationParam param);
        /// <summary>
        /// 住院费用预结算
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerHospitalizationPreSettlementDto WorkerHospitalizationPreSettlement(WorkerHospitalizationPreSettlementParam param);
        /// <summary>
        /// 职工划卡
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        WorkerStrokeCardDto WorkerStrokeCard(WorkerStrokeCardParam param);
    }
}
