using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 查询医保明细信息
/// </summary>
   public class QueryMedicalInsuranceDetailInfoDto
    {   
        public Guid Id { get; set; }
        
        /// <summary>
        /// 医保住院号
        /// </summary>
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 诊断列表
        /// </summary>
        public List<InpatientDiagnosisDto> DiagnosisList { get; set; }
        ///// <summary>
        ///// 身份证号
        ///// </summary>
        //public string IdCardNo { get; set; }
        ///// <summary>
        ///// 个人编码
        ///// </summary>
        //public string PersonNumber { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        public string AdmissionDate { get; set; }
        /// <summary>
        /// 胎儿数
        /// </summary>
        
        public string FetusNumber { get; set; }
        /// <summary>
        /// 户口性质
        /// </summary>
      
        public string HouseholdNature { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        public string InpatientDepartmentCode { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>

        public string BedNumber { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>
       
        public string HospitalizationNo { get; set; }
        /// <summary>
        /// 医保类型
        /// </summary>
        public  string InsuranceType { get; set; }


    }
}
