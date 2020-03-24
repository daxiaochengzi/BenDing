using System;
using System.Collections.Generic;
using System.Text;
using BenDing.Domain.Models.Dto.Resident;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 病人信息
/// </summary>
   public class InpatientInfoDto
    {/// <summary>
    /// 诊断列表
    /// </summary>
        public List<InpatientDiagnosisDto> DiagnosisList { get; set; }
        /// <summary>
        /// 住院id
        /// </summary>
        public string HospitalizationId { get; set; }
        /// <summary>
        /// 医院名称
        /// </summary>
        public string HospitalName { get; set; }
        /// <summary>
        /// 诊断json
        /// </summary>
        public string DiagnosisJson { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>

        public string AdmissionDate { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        
        public string LeaveHospitalDate { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        
        public string HospitalizationNo { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
      
        public string BusinessId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
       
        public string PatientName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
      
        public string IdCardNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
       
        public string PatientSex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
       
        public string Birthday { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
       
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        
        public string ContactPhone { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
      
        public string FamilyAddress { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>
      
        public string InDepartmentName { get; set; }
        /// <summary>
        /// 入院科室编码
        /// </summary>
       
        public string InDepartmentId { get; set; }
        /// <summary>
        /// 入院诊断医生
        /// </summary>
       
        public string AdmissionDiagnosticDoctor { get; set; }
        /// <summary>
        /// 入院床位
        /// </summary>
      
        public string AdmissionBed { get; set; }
        /// <summary>
        /// 入院主诊断
        /// </summary>
       
        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 入院主诊断ICD10
        /// </summary>
      
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 入院病区
        /// </summary>
       
        public string AdmissionWard { get; set; }
        /// <summary>
        /// 入院经办人
        /// </summary>
       
        public string AdmissionOperator { get; set; }
        /// <summary>
        /// 入院经办时间
        /// </summary>
        
        public string AdmissionOperateTime { get; set; }
        /// <summary>
        /// 住院总费用
        /// </summary>
       
        public string HospitalizationTotalCost { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
       
        public string Remark { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
       
        public string DocumentNo { get; set; }
      
        ///// <summary>
        ///// 出院床位
        ///// </summary>
        //public string LeaveHospitalSecondaryDiagnosisIcd10 { get; set; }
        ///// <summary>
        ///// 在院状态
        ///// </summary>

        //public string InpatientHospitalState { get; set; }
        /// <summary>
        /// 入院诊断医生编码
        /// </summary>

        public string AdmissionDiagnosticDoctorId { get; set; }

        /// <summary>
        /// 交易码
        /// </summary>
        public  string TransactionId { get; set; }
        
        ///// <summary>
        ///// 
        ///// </summary>
        //public ResidentUserInfoDto MedicalInsuranceResidentInfo { get; set; }
    }
}
