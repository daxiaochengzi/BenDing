using BenDing.Domain.Models.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Workers
{/// <summary>
/// 
/// </summary>
  public  class WorkerHospitalizationSettlementParam: WorkerBaseParam
    {/// <summary>
    /// id
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
        /// <summary>
        ///离院状态（1康复，2转院，3死亡，4其他）
        /// </summary>
        public string LeaveHospitalState { get; set; }
        /// <summary>
        /// 入院主要诊断疾病ICD-10编码
        /// </summary>
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码
        /// </summary>

        public string DiagnosisIcd10Two { get; set; }
        /// <summary>
        /// 入院诊断疾病ICD-10编码three
        /// </summary>

        public string DiagnosisIcd10Three { get; set; }
        /// <summary>
        /// 离院住诊断
        /// </summary>

        public string LeaveHospitalMainDiagnosis { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        public  string PatientName { get; set; }
    }
}
