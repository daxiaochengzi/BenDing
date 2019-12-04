using System;
using System.Collections.Generic;
using System.Text;
using BenDing.Domain.Models.Dto.Resident;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
   public class InpatientInfoJsonDto
    {/// <summary>
     /// 医院名称
     /// </summary>
        [JsonProperty(PropertyName = "医院名称")]
        public string HospitalName { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        [JsonProperty(PropertyName = "入院日期")]
        public string AdmissionDate { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        [JsonProperty(PropertyName = "出院日期")]
        public string LeaveHospitalDate { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        [JsonProperty(PropertyName = "住院号")]
        public string HospitalizationNo { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        [JsonProperty(PropertyName = "业务ID")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty(PropertyName = "姓名")]
        public string PatientName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty(PropertyName = "身份证号")]
        public string IdCardNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty(PropertyName = "性别")]
        public string PatientSex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [JsonProperty(PropertyName = "出生日期")]
        public string Birthday { get; set; }
        /// <summary>
        /// 联系人姓名
        /// </summary>
        [JsonProperty(PropertyName = "联系人姓名")]
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [JsonProperty(PropertyName = "联系电话")]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 家庭地址
        /// </summary>
        [JsonProperty(PropertyName = "家庭地址")]
        public string FamilyAddress { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>
        [JsonProperty(PropertyName = "入院科室")]
        public string InDepartmentName { get; set; }
        /// <summary>
        /// 入院科室编码
        /// </summary>
        [JsonProperty(PropertyName = "入院科室编码")]
        public string InDepartmentId { get; set; }
        /// <summary>
        /// 入院诊断医生
        /// </summary>
        [JsonProperty(PropertyName = "入院诊断医生")]
        public string AdmissionDiagnosticDoctor { get; set; }
        /// <summary>
        /// 入院床位
        /// </summary>
        [JsonProperty(PropertyName = "入院床位")]
        public string AdmissionBed { get; set; }
        /// <summary>
        /// 入院主诊断
        /// </summary>
        [JsonProperty(PropertyName = "入院主诊断")]
        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 入院主诊断ICD10
        /// </summary>
        [JsonProperty(PropertyName = "入院主诊断ICD10")]
        public string AdmissionMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 入院次诊断
        /// </summary>
        [JsonProperty(PropertyName = "入院次诊断")]
        public string AdmissionSecondaryDiagnosis { get; set; }
        /// <summary>
        /// 入院次诊断ICD10
        /// </summary>
        [JsonProperty(PropertyName = "入院次诊断ICD10")]
        public string AdmissionSecondaryDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 入院病区
        /// </summary>
        [JsonProperty(PropertyName = "入院病区")]
        public string AdmissionWard { get; set; }
        /// <summary>
        /// 入院经办人
        /// </summary>
        [JsonProperty(PropertyName = "入院经办人")]
        public string AdmissionOperator { get; set; }
        /// <summary>
        /// 入院经办时间
        /// </summary>
        [JsonProperty(PropertyName = "入院经办时间")]
        public string AdmissionOperateTime { get; set; }
        /// <summary>
        /// 住院总费用
        /// </summary>
        [JsonProperty(PropertyName = "住院总费用")]
        public string HospitalizationTotalCost { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty(PropertyName = "备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        [JsonProperty(PropertyName = "出院科室")]
        public string LeaveDepartmentName { get; set; }
        /// <summary>
        /// 出院科室编码
        /// </summary>
        [JsonProperty(PropertyName = "出院科室编码")]
        public string LeaveDepartmentId { get; set; }
        /// <summary>
        /// 出院病区
        /// </summary>
        [JsonProperty(PropertyName = "出院病区")]
        public string LeaveHospitalWard { get; set; }
        /// <summary>
        /// 出院床位
        /// </summary>
        [JsonProperty(PropertyName = "出院床位")]
        public string LeaveHospitalBed { get; set; }
        /// <summary>
        /// 出院主诊断
        /// </summary>
        [JsonProperty(PropertyName = "出院主诊断")]
        public string LeaveHospitalMainDiagnosis { get; set; }
        /// <summary>
        /// 出院主诊断ICD10
        /// </summary>
        [JsonProperty(PropertyName = "出院主诊断ICD10")]
        public string LeaveHospitalMainDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 出院次诊断
        /// </summary>
        [JsonProperty(PropertyName = "出院次诊断")]
        public string LeaveHospitalSecondaryDiagnosis { get; set; }
        /// <summary>
        /// 出院次诊断ICD10
        /// </summary>
        [JsonProperty(PropertyName = "出院次诊断ICD10")]
        public string LeaveHospitalSecondaryDiagnosisIcd10 { get; set; }
        /// <summary>
        /// 在院状态
        /// </summary>
        [JsonProperty(PropertyName = "在院状态")]
        public string InpatientHospitalState { get; set; }
        /// <summary>
        /// 入院诊断医生编码
        /// </summary>
        [JsonProperty(PropertyName = "入院诊断医生编码")]
        public string AdmissionDiagnosticDoctorId { get; set; }
        /// <summary>
        /// 入院床位编码
        /// </summary>
        [JsonProperty(PropertyName = "入院床位编码")]
        public string AdmissionBedId { get; set; }
        /// <summary>
        /// 入院病区编码
        /// </summary>
        [JsonProperty(PropertyName = "入院病区编码")]
        public string AdmissionWardId { get; set; }
        /// <summary>
        /// 出院床位编码
        /// </summary>
        [JsonProperty(PropertyName = "出院床位编码")]
        public string LeaveHospitalBedId { get; set; }
        /// <summary>
        /// 出院病区编码
        /// </summary>
        [JsonProperty(PropertyName = "出院病区编码")]
        public string LeaveHospitalWardId { get; set; }
       
        public ResidentUserInfoDto MedicalInsuranceResidentInfo { get; set; }
    }
}
