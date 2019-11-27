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
    {   /// <summary>
    /// 
    /// </summary>
        医保入院 = 0,
        /// <summary>
        /// 
        /// </summary>
        医保预结算 = 1,
        /// <summary>
        /// 
        /// </summary>
        医保结算 = 2,
    }
}
