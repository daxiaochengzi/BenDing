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
   public class HospitalOrganizationGradeEntity: IEntity<HospitalOrganizationGradeEntity>, IBenDingCreationAudited, IBenDingModificationAudited, IBenDingDeleteAudited
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 医院Id
        /// </summary>
        [DisplayName("医院Id")]
        [StringLength(100)]
        public string HospitalId { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        [DisplayName("等级")]
        public long? OrganizationGrade { get; set; }
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
        /// 删除人员
        /// </summary>
        [DisplayName("删除人员")]
        [StringLength(100)]
        public string DeleteUserId { get; set; }

    }
}
