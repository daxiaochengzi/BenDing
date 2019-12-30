using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.JsonEntity
{
    public class InpatientDetailListJsonDto
    {/// <summary>
     /// 清单明细
     /// </summary>
        [JsonProperty(PropertyName = "清单明细")]
        public List<InpatientDetailJsonDto> DetailList { get; set; }
    }

    public class InpatientDetailJsonDto
    {/// <summary>
     /// 费用明细id
     /// </summary>
        [JsonProperty(PropertyName = "完整流水号")]
        public string DetailId { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        [JsonProperty(PropertyName = "单据号")]
        public string DocumentNo { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        [JsonProperty(PropertyName = "单据类型")]
        public string DocumentType { get; set; }
        /// <summary>
        /// 项目编码
        /// </summary>
        [JsonProperty(PropertyName = "商品ID")]
        public  string DirectoryCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [JsonProperty(PropertyName = "医院项目名称")]
        public string DirectoryName { get; set; }
        /// <summary>
        /// 社保项目编码
        /// </summary>
        [JsonProperty(PropertyName = "社保项目编码")]
        public string ProjectCode { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [JsonProperty(PropertyName = "本单收费单位")]
        public string Unit { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        [JsonProperty(PropertyName = "院内收费项目剂型")]
        public string Formulation { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [JsonProperty(PropertyName = "院内收费项目规格")]
        public string Specification { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        [JsonProperty(PropertyName = "每次用量")]
        public string Dosage { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        [JsonProperty(PropertyName = "用法")]
        public string Usage { get; set; }
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
        /// 开单医生编码
        /// </summary>
        [JsonProperty(PropertyName = "开单医生编码")]
        public string BillDoctorId { get; set; }
        /// <summary>
        /// 开单医生姓名
        /// </summary>
        [JsonProperty(PropertyName = "开单医生姓名")]
        public string BillDoctorName { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary>
        [JsonProperty(PropertyName = "受单科室名称")]
        public string OperateDepartmentName { get; set; }
        /// <summary>
        /// 执行科室编码
        /// </summary>
        [JsonProperty(PropertyName = "受单科室编码")]
        public string OperateDepartmentId { get; set; }

        /// <summary>
        /// 执行医生姓名
        /// </summary>
        [JsonProperty(PropertyName = "受单医生姓名")]
        public string OperateDoctorName { get; set; }
        /// <summary>
        /// 执行医生编码
        /// </summary>
        [JsonProperty(PropertyName = "受单医生编码")]
        public string OperateDoctorId { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [JsonProperty(PropertyName = "费用发生时间")]
        public string BillTime { get; set; }
        /// <summary>
        /// 门急费用标志
        /// </summary>
        [JsonProperty(PropertyName = "门急费用标志")]
        public string DoorEmergencyFeeMark { get; set; }
        /// <summary>
        /// 医院审核标志
        /// </summary>
        [JsonProperty(PropertyName = "医院审核标志")]
        public string HospitalAuditMark { get; set; }

        /// <summary>
        /// 院外检查标志
        /// </summary>
        [JsonProperty(PropertyName = "院外检查标志")]
        public string OutHospitalInspectMark { get; set; }

    }
}
