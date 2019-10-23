using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
   public class InpatientInfoDto
    {
        [JsonProperty(PropertyName = "医院名称")]
        public string HospitalName { get; set; }
        [JsonProperty(PropertyName = "入院日期")]
        public string AdmissionDate { get; set; }
        [JsonProperty(PropertyName = "出院日期")]
        public string LeaveHospitalDate { get; set; }
        [JsonProperty(PropertyName = "住院号")]
        public string HospitalizationNo { get; set; }
        [JsonProperty(PropertyName = "业务ID")]
        public string BusinessId { get; set; }
        [JsonProperty(PropertyName = "姓名")]
        public string PatientName { get; set; }
        [JsonProperty(PropertyName = "身份证号")]
        public string IdCardNo { get; set; }
        [JsonProperty(PropertyName = "性别")]
        public string PatientSex { get; set; }
        [JsonProperty(PropertyName = "出生日期")]
        public string Birthday { get; set; }
        [JsonProperty(PropertyName = "联系人姓名")]
        public string ContactName { get; set; }
        [JsonProperty(PropertyName = "联系电话")]
        public string ContactPhone { get; set; }
        [JsonProperty(PropertyName = "家庭地址")]
        public string FamilyAddress { get; set; }
        [JsonProperty(PropertyName = "入院科室")]
        public string InDepartmentName { get; set; }
        [JsonProperty(PropertyName = "入院科室编码")]
        public string InDepartmentId { get; set; }
        [JsonProperty(PropertyName = "入院诊断医生")]
        public string AdmissionDiagnosticDoctor { get; set; }
        [JsonProperty(PropertyName = "入院床位")]
        public string AdmissionBed { get; set; }
        [JsonProperty(PropertyName = "入院主诊断")]
        public string AdmissionMainDiagnosis { get; set; }
        [JsonProperty(PropertyName = "入院主诊断ICD10")]
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        [JsonProperty(PropertyName = "入院次诊断")]
        public string AdmissionSecondaryDiagnosis { get; set; }
        [JsonProperty(PropertyName = "入院次诊断ICD10")]
        public string AdmissionSecondaryDiagnosisIcd10 { get; set; }
        [JsonProperty(PropertyName = "入院病区")]
        public string AdmissionWard { get; set; }
        [JsonProperty(PropertyName = "入院经办人")]
        public string AdmissionOperator { get; set; }
        [JsonProperty(PropertyName = "入院经办时间")]
        public string AdmissionOperateTime { get; set; }
        [JsonProperty(PropertyName = "住院总费用")]
        public string HospitalizationTotalCost { get; set; }
        [JsonProperty(PropertyName = "备注")]
        public string Remark { get; set; }
        [JsonProperty(PropertyName = "出院科室")]
        public string LeaveDepartmentName { get; set; }
        [JsonProperty(PropertyName = "出院科室编码")]
        public string LeaveDepartmentId { get; set; }
        [JsonProperty(PropertyName = "出院病区")]
        public string LeaveHospitalWard { get; set; }
        [JsonProperty(PropertyName = "出院床位")]
        public string LeaveHospitalBed { get; set; }
        [JsonProperty(PropertyName = "出院主诊断")]
        public string LeaveHospitalMainDiagnosis { get; set; }
        [JsonProperty(PropertyName = "出院主诊断ICD10")]
        public string LeaveHospitalMainDiagnosisIcd10 { get; set; }
        [JsonProperty(PropertyName = "出院次诊断")]
        public string LeaveHospitalSecondaryDiagnosis { get; set; }
        [JsonProperty(PropertyName = "出院次诊断ICD10")]
        public string LeaveHospitalSecondaryDiagnosisIcd10 { get; set; }
        [JsonProperty(PropertyName = "在院状态")]
        public string InpatientHospitalState { get; set; }
        [JsonProperty(PropertyName = "入院诊断医生编码")]
        public string AdmissionDiagnosticDoctorId { get; set; }
        [JsonProperty(PropertyName = "入院床位编码")]
        public string AdmissionBedId { get; set; }
        [JsonProperty(PropertyName = "入院病区编码")]
        public string AdmissionWardId { get; set; }
        [JsonProperty(PropertyName = "出院床位编码")]
        public string LeaveHospitalBedId { get; set; }
        [JsonProperty(PropertyName = "出院病区编码")]
        public string LeaveHospitalWardId { get; set; }

    }
}
