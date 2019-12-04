﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
   public class BaseOutpatientInfoDto
    {/// <summary>
     /// 姓名
     /// </summary>
      
        public string PatientName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
      
        public string IdCardNo { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
      
        public string PatientSex { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
     
        public string BusinessId { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
       
        public string OutpatientNumber { get; set; }
        /// <summary>
        /// 就诊日期
        /// </summary>
       
        public string VisitDate { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
       
        public string DepartmentName { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
       
        public string DepartmentId { get; set; }
        /// <summary>
        /// 诊断医生
        /// </summary>
       
        public string DiagnosticDoctor { get; set; }
        /// <summary>
        /// 诊断疾病编码
        /// </summary>
        
        public string DiagnosticDiseaseCode { get; set; }
        /// <summary>
        /// 诊断疾病名称
        /// </summary>
       
        public string DiagnosticDiseaseName { get; set; }
        /// <summary>
        /// 主要病情描述
        /// </summary>
      
        public string DiseaseDesc { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
       
        public string Operator { get; set; }
        /// <summary>
        /// 就诊总费用
        /// </summary>
       
        public decimal MedicalTreatmentTotalCost { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
       
        public string Remark { get; set; }
        /// <summary>
        /// 接诊状态
        /// </summary>
      
        public int ReceptionStatus { get; set; }

    }
}