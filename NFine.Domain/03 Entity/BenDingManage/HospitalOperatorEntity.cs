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
  public  class HospitalOperatorEntity: IEntity<HospitalOperatorEntity>, IBenDingCreationAudited, IBenDingModificationAudited,IBenDingDeleteAudited
    {/// <summary>
    /// Id
    /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 固定编号
        /// </summary>
        [DisplayName("固定编号")]
        [StringLength(20)]
        public string FixedEncoding { get; set; }
        /// <summary>
        /// 基层用户id
        /// </summary>
        [DisplayName("基层用户id")]
        [StringLength(100)]
        public string HisUserId { get; set; }
        /// <summary>
        /// His用户姓名
        /// </summary>
        [DisplayName("His用户姓名")]
        [StringLength(100)]
        public string HisUserName { get; set; }
        /// <summary>
        /// 医保账户
        /// </summary>
        [DisplayName("医保账户")]
        [StringLength(100)]
        public string MedicalInsuranceAccount { get; set; }
        /// <summary>
        /// 医保密码
        /// </summary>
        [DisplayName("医保密码")]
        [StringLength(50)]
        public string MedicalInsurancePwd { get; set; }
        /// <summary>
        /// 基层账户
        /// </summary>
        [DisplayName("基层账户")]
        [StringLength(100)]
        public string HisUserAccount { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        [DisplayName("厂商编号")]
        [StringLength(50)]
        public string ManufacturerNumber { get; set; }
        /// <summary>
        /// 基层密码
        /// </summary>
        [DisplayName("基层密码")]
        [StringLength(50)]
        public string HisUserPwd { get; set; }
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
        /// 
        /// </summary>
        [DisplayName("")]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DisplayName("更新时间")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime?  DeleteTime { get; set; }
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
