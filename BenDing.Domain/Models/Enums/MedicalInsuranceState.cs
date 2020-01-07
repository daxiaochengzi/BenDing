using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Enums
{/// <summary>
/// 医保状态
/// </summary>
    public enum MedicalInsuranceState
    {/// <summary>
     /// 医保取消结算
     /// </summary>
        MedicalInsuranceCancelSettlement = 0,
        /// <summary>
        /// 医保入院
        /// </summary>
        MedicalInsuranceHospitalized = 1,
        /// <summary>
        /// His入院
        /// </summary>
        HisHospitalized = 2,
        /// <summary>
        /// 医保预结算
        /// </summary>
        MedicalInsurancePreSettlement = 3,

        /// <summary>
        /// 医保结算
        /// </summary>
        MedicalInsuranceSettlement = 5,
        /// <summary>
        /// His结算 
        /// </summary>
        HisSettlement = 6,
    }
}
