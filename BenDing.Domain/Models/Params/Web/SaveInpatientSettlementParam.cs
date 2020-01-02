using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.Web
{
   public class SaveInpatientSettlementParam: UserBaseParam
    {/// <summary>
     /// id
     /// </summary>
        public Guid Id { get; set; }=Guid.Empty;

        /// <summary>
        /// 出院日期
        /// </summary>

        public string LeaveHospitalDate { get; set; }
        /// <summary>
        /// 出院科室编码
        /// </summary>

        public string LeaveHospitalDepartmentId { get; set; }
        /// <summary>
        /// 出院科室名称
        /// </summary>

        public string LeaveHospitalDepartmentName { get; set; }
        /// <summary>
        /// 出院床位
        /// </summary>

        public string LeaveHospitalBedNumber { get; set; }
        /// <summary>
        /// 诊断医生
        /// </summary>

        public string LeaveHospitalDiagnosticDoctor { get; set; }
        /// <summary>
        /// 出院经办人
        /// </summary>

        public string LeaveHospitalOperator { get; set; }
        /// <summary>
        /// 诊断json
        /// </summary>
        public string LeaveHospitalDiagnosisJson { get; set; }
    }
}
