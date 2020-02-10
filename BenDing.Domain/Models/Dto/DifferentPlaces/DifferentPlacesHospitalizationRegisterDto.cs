using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.DifferentPlaces
{
   public class DifferentPlacesHospitalizationRegisterDto
    {/// <summary>
     /// 个人账户余额
     /// </summary>
        
        public Decimal AccountBalance { get; set; }
        /// <summary>
        /// 医保住院号
        /// </summary>
      
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 出生日期 Birthday
        /// </summary>

       
        public string Birthday { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>

      
        public string IdCardNo { get; set; }
        /// <summary>
        /// 参保身份
        /// </summary>
     
        public string InsuredStatus { get; set; }
        /// <summary>
        /// 性别
        /// </summary>

      
        public string PatientSex { get; set; }

        /// <summary>
        /// 险种类型310:城镇职工基本医疗保险342：城乡居民基本医疗保险根据获取的险种类型，调用对应的职工或者居民接口办理入院。
        /// </summary>

       
        public string InsuranceType { get; set; }
        /// <summary>
        /// 特殊人员类别
        /// </summary>
       
        public string SpecialPersonnelType { get; set; }
        /// <summary>
        /// 参保人所属行政区域
        /// </summary>

       
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>

        
        public string PatientName { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>

        public string CompanyName { get; set; }
        /// <summary>
        /// 单位编号
        /// </summary>
      
        public string CompanyCode { get; set; }
        /// <summary>
        /// 享受待遇标识
        /// </summary>
        public string EnjoySign { get; set; }
        /// <summary>
        /// 未享受待遇原因
        /// </summary>
        public string NotEnjoyCause { get; set; }
    }
}
