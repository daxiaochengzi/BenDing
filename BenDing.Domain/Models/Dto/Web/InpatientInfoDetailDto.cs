using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Web
{
   public class InpatientInfoDetailDto
    {
        [JsonProperty(PropertyName = "住院号")]
        public string HospitalizationNo { get; set; }
        [JsonProperty(PropertyName = "费用明细ID")]
        public string CostDetailId { get; set; }
        [JsonProperty(PropertyName = "项目名称")]
        public string CostItemName { get; set; }
        [JsonProperty(PropertyName = "项目编码")]
        public string CostItemCode { get; set; }
        [JsonProperty(PropertyName = "项目类别名称")]
        public string CostItemCategoryName { get; set; }
        [JsonProperty(PropertyName = "项目类别编码")]
        public string CostItemCategoryCode { get; set; }
        [JsonProperty(PropertyName = "单位")]
        public string Unit { get; set; }
        [JsonProperty(PropertyName = "剂型")]
        public string Formulation { get; set; }
        [JsonProperty(PropertyName = "规格")]
        public string Specification { get; set; }
        [JsonProperty(PropertyName = "单价")]
        public string UnitPrice { get; set; }
        [JsonProperty(PropertyName = "数量")]
        public string Quantity { get; set; }
        [JsonProperty(PropertyName = "金额")]
        public string Amount { get; set; }
        [JsonProperty(PropertyName = "用量")]
        public string Dosage { get; set; }
        [JsonProperty(PropertyName = "用法")]
        public string Usage { get; set; }
        [JsonProperty(PropertyName = "用药天数")]
        public string MedicateDays { get; set; }
        [JsonProperty(PropertyName = "医院计价单位")]
        public string HospitalPricingUnit { get; set; }
        [JsonProperty(PropertyName = "是否进口药品")]
        public string IsImportedDrugs { get; set; }
        [JsonProperty(PropertyName = "药品产地")]
        public string DrugProducingArea { get; set; }
        [JsonProperty(PropertyName = "处方号")]
        public string RecipeCode { get; set; }
        [JsonProperty(PropertyName = "费用单据类型")]
        public string CostDocumentType { get; set; }
        [JsonProperty(PropertyName = "开单科室名称")]
        public string BillDepartment { get; set; }
        [JsonProperty(PropertyName = "开单科室编码")]
        public string BillDepartmentId { get; set; }
        [JsonProperty(PropertyName = "开单医生姓名")]
        public string BillDoctorName { get; set; }
        [JsonProperty(PropertyName = "开单医生编码")]
        public string BillDoctorId { get; set; }
        [JsonProperty(PropertyName = "开单时间")]
        public string BillTime { get; set; }
        [JsonProperty(PropertyName = "执行科室名称")]
        public string OperateDepartmentName { get; set; }
        [JsonProperty(PropertyName = "执行科室编码")]
        public string OperateDepartmentId { get; set; }
        [JsonProperty(PropertyName = "执行医生姓名")]
        public string OperateDoctorName { get; set; }
        [JsonProperty(PropertyName = "执行医生编码")]
        public string OperateDoctorId { get; set; }
        [JsonProperty(PropertyName = "执行时间")]
        public string OperateTime { get; set; }
        [JsonProperty(PropertyName = "处方医师")]
        public string PrescriptionDoctor { get; set; }
        [JsonProperty(PropertyName = "经办人")]
        public string Operators { get; set; }
        [JsonProperty(PropertyName = "执业医师证号")]
        public string PracticeDoctorNumber { get; set; }
        [JsonProperty(PropertyName = "费用冲销ID")]
        public string CostWriteOffId { get; set; }
        [JsonProperty(PropertyName = "机构编码")]
        public string OrganizationCode { get; set; }
        [JsonProperty(PropertyName = "机构名称")]
        public string OrganizationName { get; set; }

    }
}
