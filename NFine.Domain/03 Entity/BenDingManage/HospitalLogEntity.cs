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
   public class HospitalLogEntity:IEntity<HospitalLogEntity>, IBenDingCreationAudited, IBenDingModificationAudited,IBenDingDeleteAudited
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        [DisplayName("业务id")]
        [StringLength(50)]
        public string BusinessId { get; set; }
        /// <summary>
        /// 入参或者旧数据
        /// </summary>
        [DisplayName("入参或者旧数据")]
        public string JoinOrOldJson { get; set; }
        /// <summary>
        /// 回参或者新数据
        /// </summary>
        [DisplayName("回参或者新数据")]
        public string ReturnOrNewJson { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 组织机构
        /// </summary>
        [DisplayName("组织机构")]
        [StringLength(50)]
        public string OrganizationCode { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        [DisplayName("组织机构名称")]
        [StringLength(500)]
        public string OrganizationName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        [DisplayName("删除标记")]
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
        /// 创建人员
        /// </summary>
        [DisplayName("创建人员")]
        [StringLength(100)]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 更新人员
        /// </summary>
        [DisplayName("更新人员")]
        [StringLength(100)]
        public string UpdateUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        [StringLength(100)]
        public string DeleteUserId { get; set; }
    }
}
