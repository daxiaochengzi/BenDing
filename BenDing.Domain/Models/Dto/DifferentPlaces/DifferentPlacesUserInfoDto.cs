using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.DifferentPlaces
{/// <summary>
/// 异地用户资料
/// </summary>
   public class DifferentPlacesUserInfoDto
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
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险根据获取的险种类型，调用对应的职工或者居民接口办理入院。
        /// </summary>
        public string InsuranceType { get; set; }
        /// <summary>
        /// 参保状态
        /// </summary>       
        public string InsuredState { get; set; }
        /// <summary>
        /// 医院住院报销状态
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
        /// 单位名称
        /// </summary>

       
        public string CompanyName { get; set; }

        /// <summary>
        /// 统筹支付余额
        /// </summary>
       
        public decimal OverallPaymentBalance { get; set; }
        /// <summary>
        /// 职工类别
        /// </summary>
       
        public string WorkersType { get; set; }
        /// <summary>
        /// 特殊人员身份 
        /// </summary>
       
        public string SpecialPersonnel { get; set; }
        /// <summary>
        /// 个人账户余额
        /// </summary>
       
        public Decimal AccountBalance { get; set; }

    }
}
