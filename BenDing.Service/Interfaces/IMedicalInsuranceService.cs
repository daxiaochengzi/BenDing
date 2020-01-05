using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Params.UI;

namespace BenDing.Service.Interfaces
{
    public  interface IMedicalInsuranceService
    {  /// <summary>
        /// 入院登记
        /// </summary>
        /// <returns></returns>
        void HospitalizationRegister(ResidentHospitalizationRegisterUiParam param);
        /// <summary>
        /// 入院登记修改
        /// </summary>
        /// <param name="param"></param>
        void HospitalizationModify(HospitalizationModifyUiParam param);
    }
}
