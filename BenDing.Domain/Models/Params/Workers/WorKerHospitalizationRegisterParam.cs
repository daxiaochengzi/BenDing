using BenDing.Domain.Models.Dto.Base;
using BenDing.Domain.Models.Dto.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.Workers
{/// <summary>
/// 
/// </summary>
  public  class WorKerHospitalizationRegisterParam: WorkerBaseParam
    {
        public Guid Id { get; set; }

        ///<summary>
        /// 传入标志(为’1’表示卡号,’2’身份证号,3为个人编号)
        /// </summary>

        public string  AfferentSign{ get; set; }
        /// <summary>
        ///  身份标识 (个人编号 IC卡号 身份证号)
        /// </summary>

        public string IdentityMark { get; set; }
        /// <summary>
        /// 医疗类别(普通住院 21，工伤住院41)
        /// </summary>

        public string MedicalCategory { get; set; }

        /// <summary>
        /// 入院日期(格式为yyyyMMdd)
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
        /// 入院诊断疾病名称
        /// </summary>
      
        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 住院科室编号 
        /// </summary>
       
        public string InpatientDepartmentCode { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
     
        public string BedNumber { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        public string InpatientArea { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>

       public string HospitalizationNo { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string Operators { get; set; }

        //public List<InpatientDiagnosisDto> DiagnosisList { get; set; } = null;


    }
}
