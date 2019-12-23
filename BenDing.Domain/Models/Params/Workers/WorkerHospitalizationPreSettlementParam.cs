using BenDing.Domain.Models.Dto.Base;
using BenDing.Domain.Models.Params.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Workers
{/// <summary>
/// 
/// </summary>
  public  class WorkerHospitalizationPreSettlementParam: WorkerBaseParam
        {/// <summary>
        /// 医保id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 是否保存住院次数
        /// </summary>
        public string IsHospitalizationFrequency { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string Operators { get; set; }
        /// <summary>
        /// 离院日期(yyyyMMdd)
        /// </summary>
        public string LeaveHospitalDate { get; set; }
    }
}
