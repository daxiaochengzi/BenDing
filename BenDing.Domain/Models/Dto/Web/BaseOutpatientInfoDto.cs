using System;
using System.Collections.Generic;
using System.Text;
using BenDing.Domain.Models.Dto.JsonEntity;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 
/// </summary>
   public class BaseOutpatientInfoDto
    {/// <summary>
    /// id
    /// </summary>
        public Guid Id { get; set; }=Guid.Empty;
          /// <summary>
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
        /// 发票号
        /// </summary>
      
        public string InvoiceNo { get; set; }
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
        /// 接诊状态0未截至、1接诊中、2已接诊
        /// </summary>

        public int ReceptionStatus { get; set; }
        /// <summary>
        /// 诊断json
        /// </summary>
        public  string DiagnosticJson { get; set; }
       

        /// <summary>
        /// 诊断列表
        /// </summary>

        public List<InpatientDiagnosisDataDto> DiagnosisList { get; set; } = null;

    }
}
