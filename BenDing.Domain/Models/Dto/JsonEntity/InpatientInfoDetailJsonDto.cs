using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{
   public class InpatientInfoDetailJsonDto
    {
        /// <summary>
        /// 住院号
        /// </summary>
        [JsonProperty(PropertyName = "住院号")]
        public string HospitalizationNo { get; set; }
        /// <summary>
        /// 费用明细ID
        /// </summary>
        [JsonProperty(PropertyName = "费用明细ID")]
        public string CostDetailId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [JsonProperty(PropertyName = "项目名称")]
        public string DirectoryName { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        [JsonProperty(PropertyName = "项目编码")]
        public string DirectoryCode { get; set; }
        /// <summary>
        /// 项目类别名称
        /// </summary>
        [JsonProperty(PropertyName = "项目类别名称")]
        public string DirectoryCategoryName { get; set; }
        /// <summary>
        /// 项目类别编码
        /// </summary>
        [JsonProperty(PropertyName = "项目类别编码")]
        public string DirectoryCategoryCode { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [JsonProperty(PropertyName = "单位")]
        public string Unit { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        [JsonProperty(PropertyName = "剂型")]
        public string Formulation { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [JsonProperty(PropertyName = "规格")]
        public string Specification { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [JsonProperty(PropertyName = "单价")]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [JsonProperty(PropertyName = "数量")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [JsonProperty(PropertyName = "金额")]
        public decimal Amount { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        [JsonProperty(PropertyName = "用量")]
        public string Dosage { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        [JsonProperty(PropertyName = "用法")]
        public string Usage { get; set; }
        /// <summary>
        /// 用药天数
        /// </summary>
        [JsonProperty(PropertyName = "用药天数")]
        public string MedicateDays { get; set; }
        /// <summary>
        /// 医院计价单位
        /// </summary>
        [JsonProperty(PropertyName = "医院计价单位")]
        public string HospitalPricingUnit { get; set; }
        /// <summary>
        /// 是否进口药品
        /// </summary>
        [JsonProperty(PropertyName = "是否进口药品")]
        public string IsImportedDrugs { get; set; }
        /// <summary>
        /// 药品产地
        /// </summary>
        [JsonProperty(PropertyName = "药品产地")]
        public string DrugProducingArea { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        [JsonProperty(PropertyName = "处方号")]
        public string RecipeCode { get; set; }
        /// <summary>
        /// 费用单据类型
        /// </summary>
        [JsonProperty(PropertyName = "费用单据类型")]
        public string CostDocumentType { get; set; }
        /// <summary>
        /// 开单科室名称
        /// </summary>
        [JsonProperty(PropertyName = "开单科室名称")]
        public string BillDepartment { get; set; }
        /// <summary>
        /// 开单科室编码
        /// </summary>
        [JsonProperty(PropertyName = "开单科室编码")]
        public string BillDepartmentId { get; set; }
        /// <summary>
        /// 开单医生姓名
        /// </summary>
        [JsonProperty(PropertyName = "开单医生姓名")]
        public string BillDoctorName { get; set; }
        /// <summary>
        /// 开单医生编码
        /// </summary>
        [JsonProperty(PropertyName = "开单医生编码")]
        public string BillDoctorId { get; set; }
        /// <summary>
        /// 开单时间
        /// </summary>
        [JsonProperty(PropertyName = "开单时间")]
        public string BillTime { get; set; }
        /// <summary>
        /// 执行科室名称
        /// </summary>
        [JsonProperty(PropertyName = "执行科室名称")]
        public string OperateDepartmentName { get; set; }
        /// <summary>
        /// 执行科室编码
        /// </summary>
        [JsonProperty(PropertyName = "执行科室编码")]
        public string OperateDepartmentId { get; set; }
        /// <summary>
        /// 执行医生姓名
        /// </summary>
        [JsonProperty(PropertyName = "执行医生姓名")]
        public string OperateDoctorName { get; set; }
        /// <summary>
        /// 执行医生编码
        /// </summary>
        [JsonProperty(PropertyName = "执行医生编码")]
        public string OperateDoctorId { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        [JsonProperty(PropertyName = "执行时间")]
        public string OperateTime { get; set; }
        /// <summary>
        /// 处方医师
        /// </summary>
        [JsonProperty(PropertyName = "处方医师")]
        public string PrescriptionDoctor { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        [JsonProperty(PropertyName = "经办人")]
        public string Operators { get; set; }
        /// <summary>
        /// 执业医师证号
        /// </summary>
        [JsonProperty(PropertyName = "执业医师证号")]
        public string PracticeDoctorNumber { get; set; }
        /// <summary>
        /// 费用冲销ID
        /// </summary>
        [JsonProperty(PropertyName = "费用冲销ID")]
        public string CostWriteOffId { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [JsonProperty(PropertyName = "机构编码")]
        public string OrganizationCode { get; set; }
        /// <summary>
        /// OrganizationName
        /// </summary>
        [JsonProperty(PropertyName = "机构名称")]
        public string OrganizationName { get; set; }
    }
}
