using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
    public class BaseOutpatientDetailDto
    {
        /// <summary>
        /// 门诊号
        /// </summary>
       
        public string OutpatientNo { get; set; }
        /// <summary>
        /// 费用明细ID
        /// </summary>
      
        public string DetailId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
       
        public string DirectoryName { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
       
        public string DirectoryCode { get; set; }
        /// <summary>
        /// 项目类别名称
        /// </summary>
        
        public string DirectoryCategoryName { get; set; }
        /// <summary>
        /// 项目类别编码
        /// </summary>
        
        public string DirectoryCategoryCode { get; set; }
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
        /// 用量
        /// </summary>
      
        public string Dosage { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
       
        public string Usage { get; set; }
        /// <summary>
        /// 用药天数
        /// </summary>
      
        public string MedicateDays { get; set; }
        /// <summary>
        /// 医院计价单位
        /// </summary>
        
        public string HospitalPricingUnit { get; set; }
        /// <summary>
        /// 是否进口药品
        /// </summary>
       
        public string IsImportedDrugs { get; set; }
        /// <summary>
        /// 药品产地
        /// </summary>
      
        public string DrugProducingArea { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        
        public string RecipeCode { get; set; }
        /// <summary>
        /// 费用单据类型
        /// </summary>
        
        public string CostDocumentType { get; set; }
        /// <summary>
        /// 开单科室名称
        /// </summary>
       
        public string BillDepartment { get; set; }
        /// <summary>
        /// 开单科室编码
        /// </summary>
       
        public string BillDepartmentId { get; set; }
        /// <summary>
        /// 开单医生姓名
        /// </summary>
       
        public string BillDoctorName { get; set; }
        /// <summary>
        /// 开单医生编码
        /// </summary>
        
        public string BillDoctorId { get; set; }
        /// <summary>
        /// 开单时间
        /// </summary>
       
        public string BillTime { get; set; }
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
        
        public string OperateTime { get; set; }
        /// <summary>
        /// 处方医师
        /// </summary>
        
        public string PrescriptionDoctor { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
       
        public string Operators { get; set; }
        /// <summary>
        /// 执业医师证号
        /// </summary>
      
        public string PracticeDoctorNumber { get; set; }
        /// <summary>
        /// 费用冲销ID
        /// </summary>
       
        public string CostWriteOffId { get; set; }
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
