using System;
using BenDing.Domain.Models.Dto.Base;
using BenDing.Domain.Models.Dto.Web;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.Workers
{/// <summary>
 /// 职工住院修改入参
 /// </summary>
    public   class ModifyWorkerHospitalizationParam:WorkerBaseParam
    {/// <summary>
     /// Id
     /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 入院日期(格式为YYYYMMDD)
        /// </summary>
        public string AdmissionDate { get; set; }
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
        /// 入院诊断
        /// </summary>

        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        public string InpatientArea { get; set; }
        /// <summary>
        /// 科室编号
        /// </summary>
        public string InpatientDepartmentCode { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>

        public string BedNumber { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>

        public string HospitalizationNo { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string Operators { get; set; }

    }
}
