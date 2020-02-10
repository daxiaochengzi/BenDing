using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NFine.Domain.BenDing.Infrastructure;

namespace NFine.Domain._03_Entity.BenDingManage
{
  public  class HospitalizationFeeEntity:IBenDingEntity<InpatientEntity>, IBenDingCreationAudited,IBenDingDeleteAudited, IBenDingModificationAudited
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 住院id
        /// </summary>
        [DisplayName("住院id")]
        [StringLength(32)]
        public string HospitalizationId { get; set; }
        /// <summary>
        /// 费用明细ID
        /// </summary>
        [DisplayName("费用明细ID")]
        [StringLength(50)]
        public string DetailId { get; set; }
        /// <summary>
        /// 处方号
        /// </summary>
        [DisplayName("处方号")]
        [StringLength(20)]
        public string DocumentNo { get; set; }
        /// <summary>
        /// 开单科室名称
        /// </summary>
        [DisplayName("开单科室名称")]
        [StringLength(100)]
        public string BillDepartment { get; set; }
        /// <summary>
        /// 项目名称[费用]
        /// </summary>
        [DisplayName("项目名称[费用]")]
        [StringLength(50)]
        public string DirectoryName { get; set; }
        /// <summary>
        /// 项目编码[费用]
        /// </summary>
        [DisplayName("项目编码[费用]")]
        [StringLength(100)]
        public string DirectoryCode { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        [DisplayName("批次号")]
        [StringLength(20)]
        public string BatchNumber { get; set; }
      
        /// <summary>
        /// 社保项目编码
        /// </summary>
        [DisplayName("社保项目编码")]
        [StringLength(100)]
        public string ProjectCode { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        [DisplayName("剂型")]
        [StringLength(100)]
        public string Formulation { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [DisplayName("规格")]
        [StringLength(100)]
        public string Specification { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [DisplayName("单价")]
        public decimal? UnitPrice { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        [DisplayName("用法")]
        [StringLength(100)]
        public string Usage { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public int? Quantity { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// 费用单据类型
        /// </summary>
        [DisplayName("费用单据类型")]
        [StringLength(100)]
        public string DocumentType { get; set; }
        /// <summary>
        /// 开单科室编码
        /// </summary>
        [DisplayName("开单科室编码")]
        [StringLength(50)]
        public string BillDepartmentId { get; set; }
        /// <summary>
        /// 开单医生编码
        /// </summary>
        [DisplayName("开单医生编码")]
        [StringLength(50)]
        public string BillDoctorId { get; set; }
        /// <summary>
        /// 开单医生姓名
        /// </summary>
        [DisplayName("开单医生姓名")]
        [StringLength(100)]
        public string BillDoctorName { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        [DisplayName("用量")]
        [StringLength(100)]
        public string Dosage { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        [StringLength(100)]
        public string Unit { get; set; }
        /// <summary>
        /// 开单时间
        /// </summary>
        [DisplayName("开单时间")]
        public DateTime? BillTime { get; set; }
        /// <summary>
        /// 执行科室名称
        /// </summary>
        [DisplayName("执行科室名称")]
        [StringLength(50)]
        public string OperateDepartmentName { get; set; }
        /// <summary>
        /// 执行科室编码
        /// </summary>
        [DisplayName("执行科室编码")]
        [StringLength(50)]
        public string OperateDepartmentId { get; set; }
        /// <summary>
        /// 执行医生姓名
        /// </summary>
        [DisplayName("执行医生姓名")]
        [StringLength(100)]
        public string OperateDoctorName { get; set; }
        /// <summary>
        /// 执行医生编码
        /// </summary>
        [DisplayName("执行医生编码")]
        [StringLength(50)]
        public string OperateDoctorId { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        [DisplayName("执行时间")]
        public DateTime? OperateTime { get; set; }
        /// <summary>
        /// 门急费用标志
        /// </summary>
        [DisplayName("门急费用标志")]
        [StringLength(2)]
        public string DoorEmergencyFeeMark { get; set; }
        /// <summary>
        /// 医院审核标志
        /// </summary>
        [DisplayName("医院审核标志")]
        [StringLength(2)]
        public string HospitalAuditMark { get; set; }
        /// <summary>
        /// 院外检查标志
        /// </summary>
        [DisplayName("院外检查标志")]
        [StringLength(10)]
        public string OutHospitalInspectMark { get; set; }
        /// <summary>
        /// 机构编码[取接口30返回的ID]
        /// </summary>
        [DisplayName("机构编码[取接口30返回的ID]")]
        [StringLength(50)]
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        [DisplayName("机构名称")]
        [StringLength(100)]
        public string OrganizationName { get; set; }
        /// <summary>
        /// 上传标志
        /// </summary>
        [DisplayName("上传标志")]
        public int? UploadMark { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [DisplayName("序号")]
        public long? DataSort { get; set; }
        /// <summary>
        /// 调整差值
        /// </summary>
        [DisplayName("调整差值")]
        public decimal? AdjustmentDifferenceValue { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        [DisplayName("上传时间")]
        public DateTime? UploadTime { get; set; }
        /// <summary>
        /// 上传人员名称
        /// </summary>
        [DisplayName("上传人员名称")]
        [StringLength(100)]
        public string UploadUserName { get; set; }
        /// <summary>
        /// 医保上传金额
        /// </summary>
        [DisplayName("医保上传金额")]
        public decimal? UploadAmount { get; set; }
        /// <summary>
        /// 营业时间
        /// </summary>
        [DisplayName("营业时间")]
        public DateTime? BusinessTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DisplayName("更新时间")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 删除标记[0:默认,1:删除]
        /// </summary>
        [DisplayName("删除标记[0:默认,1:删除]")]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [DisplayName("删除时间")]
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 操作员ID-[创建]
        /// </summary>
        [DisplayName("操作员ID-[创建]")]
        [StringLength(100)]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 操作员ID-[删除]
        /// </summary>
        [DisplayName("操作员ID-[删除]")]
        [StringLength(100)]
        public string DeleteUserId { get; set; }
        /// <summary>
        /// 更新人员
        /// </summary>
        [DisplayName("更新人员")]
        [StringLength(100)]
        public string UpdateUserId { get; set; }
        /// <summary>
        /// 上传人员id
        /// </summary>
        [DisplayName("上传人员id")]
        [StringLength(100)]
        public string UploadUserId { get; set; }

    }
}
