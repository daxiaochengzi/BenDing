using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Resident
{

    public class ResidentUserInfoDto
    {/// <summary>
     /// 个人编码
     /// </summary>
        public string PersonalCoding { get; set; }

        /// <summary>
        /// 行政区域
        /// </summary>
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PatientName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string PatientSex { get; set; }
        /// <summary>
        /// 出生日期 Birthday
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险
        /// </summary>
        public string InsuranceType { get; set; }
        /// <summary>
        /// 参保状态
        /// </summary>
        public string InsuredState { get; set; }
        /// <summary>
        /// 报销状态
        /// </summary>
        public string ReimbursementStatus { get; set; }
        /// <summary>
        /// 报销状态说明
        /// </summary>
        public string ReimbursementStatusExplain { get; set; }
        /// <summary>
        /// 人员分类
        /// </summary>
        public string PersonnelClassification { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string ContactAddress { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }
        /// <summary>
        /// 实足年龄
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 社区名称
        /// </summary>
        public string CommunityName { get; set; }
        /// <summary>
        /// 居民医保账户余额ResidentInsuranceBalance
        /// </summary>
        public decimal ResidentInsuranceBalance { get; set; }
        /// <summary>
        /// 职工医保账户余额 
        /// </summary>
        public decimal WorkersInsuranceBalance { get; set; }
        /// <summary>
        /// 门特余额
        /// </summary>
        public decimal MentorBalance { get; set; }
        /// <summary>
        /// 建卡贫困户标志
        /// </summary>
        public string PoorMark { get; set; }
        /// <summary>
        /// 重点救助对象类别
        /// </summary>
        public string RescueType { get; set; }
        /// <summary>
        /// 重点优抚对象类别
        /// </summary>
        public string PreferentialTreatmentType { get; set; }
        /// <summary>
        /// 民政特殊人员认定地
        /// </summary>
        public string SpecialPeopleCognizancePlace { get; set; }
        /// <summary>
        /// 统筹支付余额
        /// </summary>
        public decimal OverallPaymentBalance { get; set; }
    }
}
