using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Dto.Resident
{
   public class HisHospitalizationSettlementCancelDto
    {

        /// <summary>
        /// 入院日期
        /// </summary>

        public string AdmissionDate { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>

        public string LeaveHospitalDate { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>

        public string HospitalizationNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>

        public string PatientName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>

        public string IdCardNo { get; set; }
        /// <summary>
        /// 入院科室
        /// </summary>

        public string InDepartmentName { get; set; }
       
        /// <summary>
        /// 入院诊断医生
        /// </summary>

        public string AdmissionDiagnosticDoctor { get; set; }

        /// <summary>
        /// 入院经办人
        /// </summary>

        public string AdmissionOperator { get; set; }
        /// <summary>
        /// 结算编号
        /// </summary>
      
       public  string SettlementNo { get; set; }
        /// <summary>
        /// 就诊编号
        /// </summary>

        public string DiagnosisNo { get; set; }
        /// <summary>
        ///取消经办人
        /// </summary>
        public string CancelOperator { get; set; }
        /// <summary>
        /// 是否生育入院
        /// </summary>
        public int IsBirthHospital { get; set; }

        /// <summary>
        /// 医保类型(居民，职工,异地)
        /// </summary>
        public string InsuranceType { get; set; }

        /// <summary>
        /// 出院诊断
        /// </summary>
        public List<InpatientDiagnosisDto> DiagnosisList { get; set; } = null;
    }
}
