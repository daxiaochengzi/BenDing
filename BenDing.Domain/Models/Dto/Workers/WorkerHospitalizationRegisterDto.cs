using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Workers
{/// <summary>
/// 
/// </summary>
    public class WorkerHospitalizationRegisterDto
    { /// <summary>
      /// 审批编号
      /// </summary>
        public string ApprovalNumber { get; set; }
        /// <summary>
        /// 社保住院号
        /// </summary>
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 年住院次数
        /// </summary>
        public string YearHospitalizationNumber { get; set; }
        /// <summary>
        /// 统筹已付金额
        /// </summary>
        public string OverallPlanningAlreadyAmount { get; set; }
        /// <summary>
        /// 统筹可付金额
        /// </summary>
        public string OverallPlanningCanAmount { get; set; }
    }
}
