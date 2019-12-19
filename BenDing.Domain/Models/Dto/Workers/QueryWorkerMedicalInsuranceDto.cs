using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Dto.Workers
{
   public class QueryWorkerMedicalInsuranceDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// 医疗类别(普通住院 21，工伤住院41)
        /// </summary>

        public string MedicalCategory { get; set; }
        /// <summary>
        /// 医保住院号
        /// </summary>
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 诊断列表
        /// </summary>
        public List<InpatientDiagnosisDto> DiagnosisList { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCardNo { get; set; }
        /// <summary>
        /// 个人编码
        /// </summary>
        public string PersonNumber { get; set; }
        /// <summary>
        /// 职工卡号
        /// </summary>
        public string WorkerCardNo { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        public string AdmissionDate { get; set; }

        /// <summary>
        /// 床位号
        /// </summary>

        public string BedNumber { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>

        public string HospitalizationNo { get; set; }
    }
}
