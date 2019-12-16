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
        void WorkerHospitalizationRegister(WorKerHospitalizationRegisterParam param);
    }
}
