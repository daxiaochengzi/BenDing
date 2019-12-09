using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
   public class InpatientInfoDetailDto
    {
        /// <summary>
        /// 费用明细id
        /// </summary>
     
        public string DetailId { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
      
        public string DocumentNo { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
     
        public string DocumentType { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
      
        public string DirectoryCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
      
        public string DirectoryName { get; set; }
        /// <summary>
        /// 社保项目编码
        /// </summary>
       
        public string ProjectCode { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
       
        public string Unit { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
       
        public string Formulation { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
      
        public string Specification { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
     
        public string Dosage { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
       
        public string Usage { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
       
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
       
        public decimal Quantity { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
      
        public decimal Amount { get; set; }

        /// <summary>
        /// 开单科室名称
        /// </summary>
       
        public string BillDepartment { get; set; }
        /// <summary>
        /// 开单科室编码
        /// </summary>
      
        public string BillDepartmentId { get; set; }

        /// <summary>
        /// 开单医生编码
        /// </summary>
       
        public string BillDoctorId { get; set; }
        /// <summary>
        /// 开单医生姓名
        /// </summary>
       
        public string BillDoctorName { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary>
       
        public string OperateDepartmentName { get; set; }
        /// <summary>
        /// 执行科室编码
        /// </summary>
      
        public string OperateDepartmentId { get; set; }

        /// <summary>
        /// 执行医生姓名
        /// </summary>
        
        public string OperateDoctorName { get; set; }
        /// <summary>
        /// 执行医生编码
        /// </summary>
       
        public string OperateDoctorId { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        
        public string BillTime { get; set; }
        /// <summary>
        /// 门急费用标志
        /// </summary>
       
        public string DoorEmergencyFeeMark { get; set; }
        /// <summary>
        /// 医院审核标志
        /// </summary>
        
        public string HospitalAuditMark { get; set; }

        /// <summary>
        /// 院外检查标志
        /// </summary>
      
        public string OutHospitalInspectMark { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrganizationCode { get; set; }
            /// <summary>
            /// OrganizationName
            /// </summary>
           
        public string OrganizationName { get; set; }

        }
    }

