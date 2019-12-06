using NFine.Domain.BenDing.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.BenDingManage
{
   public class MedicalInsuranceEntity: IEntity<MedicalInsuranceEntity>, IBenDingCreationAudited,IBenDingDeleteAudited, IBenDingModificationAudited
    {/// <summary>
    /// Id
    /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        [DisplayName("业务ID")]
        [StringLength(50)]
        public string HisHospitalizationId { get; set; }
        /// <summary>
        /// 医保卡号
        /// </summary>
        [DisplayName("医保卡号")]
        [StringLength(50)]
        public string InsuranceNo { get; set; }
        /// <summary>
        /// 医保总费用
        /// </summary>
        [DisplayName("医保总费用")]
        public decimal? MedicalInsuranceYearBalance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        [StringLength(20)]
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 自付费用
        /// </summary>
        [DisplayName("自付费用")]
        public decimal? SelfPayFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        public string AdmissionInfoJson { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        public decimal? ReimbursementExpenses { get; set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        [DisplayName("其他信息")]
        public string OtherInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        [StringLength(100)]
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        [StringLength(100)]
        public string OrganizationName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        public long? InsuranceType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 删除标记[0:默认,1:删除]
        /// </summary>
        [DisplayName("删除标记[0:默认,1:删除]")]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DisplayName("更新时间")]
        public DateTime? UpdateTime { get; set; }
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
        /// 
        /// </summary>
        [DisplayName("")]
        [StringLength(100)]
        public string UpdateUserId { get; set; }
    }
}
