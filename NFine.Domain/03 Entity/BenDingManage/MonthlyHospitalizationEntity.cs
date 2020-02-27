using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFine.Domain.BenDing.Infrastructure;

namespace NFine.Domain._03_Entity.BenDingManage
{
  public  class MonthlyHospitalizationEntity:IBenDingEntity<InpatientEntity>, IBenDingCreationAudited,IBenDingDeleteAudited, IBenDingModificationAudited
    {/// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 组织机构编码
        /// </summary>
        [DisplayName("组织机构编码")]
        [StringLength(50)]
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        [DisplayName("组织机构名称")]
        [StringLength(500)]
        public string OrganizationName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [DisplayName("开始时间")]
       
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayName("结束时间")]
      
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        [DisplayName("人员类别")]
        [StringLength(20)]
        public string PeopleType { get; set; }
        /// <summary>
        /// 汇总类别
        /// </summary>
        [DisplayName("汇总类别")]
        [StringLength(20)]
        public string SummaryType { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        [DisplayName("单据号")]
        [StringLength(100)]
        public string DocumentNo { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        [DisplayName("人数")]
        public long? PeopleNum { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [DisplayName("金额")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 是否撤销
        /// </summary>
        [DisplayName("是否撤销")]
        public bool IsRevoke { get; set; }
        
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
        /// 创建人员id
        /// </summary>
        [DisplayName("创建人员id")]
        [StringLength(100)]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 更新人员id
        /// </summary>
        [DisplayName("更新人员id")]
        [StringLength(100)]
        public string UpdateUserId { get; set; }
        /// <summary>
        /// 删除人员id
        /// </summary>
        [DisplayName("删除人员id")]
        [StringLength(100)]
        public string DeleteUserId { get; set; }
    }
}
