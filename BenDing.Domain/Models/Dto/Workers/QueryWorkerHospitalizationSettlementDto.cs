using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Resident;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Dto.Workers
{
    [XmlRoot("ROWDATA", IsNullable = false)]
    public class QueryWorkerHospitalizationSettlementDto
    {

        [XmlElementAttribute("ROW")]
        [JsonProperty(PropertyName = "Row")]
        public List<QueryWorkerHospitalizationSettlementDataRowDto> Row { get; set; }
    }
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class QueryWorkerHospitalizationSettlementDataRowDto
    {/// <summary>
    /// 序号
    /// </summary>
        [XmlElementAttribute("C0", IsNullable = false)]
        public  int SerialNumber  { get; set; }
        /// <summary>
        /// 医保住院号
        /// </summary>
        [XmlElementAttribute("AKC190", IsNullable = false)]
        public string MedicalInsuranceHospitalizationNo { get; set; }

        /// <summary>
        /// 医疗人员类别
        /// </summary>
        [XmlElementAttribute("AKC021", IsNullable = false)]
        public int MedicalCategory { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        [XmlElementAttribute("AAC001", IsNullable = false)]

        public string PersonNumber { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        [XmlElementAttribute("AAC002", IsNullable = false)]
        public  string IdCardNo { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        [XmlElementAttribute("AAC003", IsNullable = false)]
        public  string PatientName { get; set; }
        /// <summary>
        /// 病人性别
        /// </summary>
        [XmlElementAttribute("AAC004", IsNullable = false)]
        public  int PatientSex { get; set; }
        /// <summary>
        /// 单位编号
        /// </summary>
        [XmlElementAttribute("AAB001", IsNullable = false)]
        public string UnitNumber { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        [XmlElementAttribute("AAB004", IsNullable = false)]
        public string UnitName{get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        [XmlElementAttribute("AKC192", IsNullable = false)]
     
        public string AdmissionDate { get; set; }

        /// <summary>
        /// 入院诊断
        /// </summary>
        [XmlElementAttribute("AKC193", IsNullable = false)]

        public string AdmissionMainDiagnosis { get; set; }
        /// <summary>
        /// 离院日期
        /// </summary>
        [XmlElementAttribute("AKC194", IsNullable = false)]
        public string LeaveHospitalDate { get; set; }
        /// <summary>
        /// 住院情况
        /// </summary>
        [XmlElementAttribute("AKC195", IsNullable = false)]
        public string HospitalizationSituation { get; set; }
        /// <summary>
        /// 出院诊断
        /// </summary>
        [XmlElementAttribute("AKC196", IsNullable = false)]
        public string LeaveHospitalDiagnosis { get; set; }
        /// <summary>
        /// 病区
        /// </summary>
        [XmlElementAttribute("AKC580", IsNullable = false)]

        public  string InpatientArea { get; set; }
        /// <summary>
        /// 床位号
        /// </summary>
        [XmlElementAttribute("AKC581", IsNullable = false)]
        public string BedNumber { get; set; }
        /// <summary>
        /// 医院住院号
        /// </summary>
        [XmlElementAttribute("AKC581", IsNullable = false)]
        public string HospitalizationNo { get; set; }
        /// <summary>
        ///结算单据号
        /// </summary>
        [XmlElementAttribute("AAE072", IsNullable = false)]
        public string SettlementNo { get; set; }
        /// <summary>
        /// 结算时间
        /// </summary>
        [XmlElementAttribute("AAE040", IsNullable = false)]
        public string SettlementTime { get; set; }
        /// <summary>
        /// 分段自付比例
        /// </summary>
        [XmlElementAttribute("AKA162", IsNullable = false)]
        public  string SubsectionSelfPayProportion { get; set; }
        /// <summary>
        /// 医疗费总额
        /// </summary>
        [XmlElementAttribute("AKC264", IsNullable = false)]
        public decimal AllAmount { get; set; }
        /// <summary>
        /// 统筹应付金额
        /// </summary>
        [XmlElementAttribute("AKC260", IsNullable = false)]
        public decimal OverallPlanningShouldPayAmount { get; set; }
        /// <summary>
        /// 统筹实付金额
        /// </summary>
        [XmlElementAttribute("AKC270", IsNullable = false)]
        public decimal OverallPlanningActualPayAmount { get; set; }
        /// <summary>
        /// 现金支付
        /// </summary>
        [XmlElementAttribute("AKC261", IsNullable = false)]
        public decimal CashPayment { get; set; }

        /// <summary>
        /// 账户支付
        /// </summary>
        [XmlElementAttribute("AKC262", IsNullable = false)]
        public decimal AccountPayment { get; set; }

        /// <summary>
        /// 公务员补贴
        /// </summary>
        [XmlElementAttribute("AKC269", IsNullable = false)]
        public decimal CivilServantsSubsidies { get; set; }
        /// <summary>
        /// 公务员补助
        /// </summary>
        [XmlElementAttribute("AKC271", IsNullable = false)]
        public decimal CivilServantsSubsidy { get; set; }
        /// <summary>
        /// 起付金额
        /// </summary>
        [XmlElementAttribute("AKC256", IsNullable = false)]
       
        public decimal PaidAmount { get; set; }
        /// <summary>
        /// 分段自付金额
        /// </summary>
        [XmlElementAttribute("AKC257", IsNullable = false)]
        public decimal SubsectionSelfPayAmount { get; set; }
        /// <summary>
        ///特检特治自付
        /// </summary>
        [XmlElementAttribute("AKC263", IsNullable = false)]
        public decimal SpecialTreatmentSelfPayAmount { get; set; }
        /// <summary>
        /// 医保除外自付金额
        /// </summary>
        [XmlElementAttribute("AKC265", IsNullable = false)]
        public  decimal MedicalInsuranceExceptSelfPayAmount { get; set; }
        /// <summary>
        /// 血液物品自付金额
        /// </summary>
        [XmlElementAttribute("AKC265", IsNullable = false)]
        public decimal BloodGoodsSelfPayAmount { get; set; }

        /// <summary>
        /// 年住院次数
        /// </summary>
        [XmlElementAttribute("AKC241", IsNullable = false)]
        public int YearHospitalizationNumber { get; set; }
        /// <summary>
        /// 本次住院次数
        /// </summary>
        [XmlElementAttribute("AKC241", IsNullable = false)]
        public int ThisHospitalizationNumber { get; set; }
        /// <summary>
        /// 交接业务号
        /// </summary>
        [XmlElementAttribute("AKC560", IsNullable = false)]
        public decimal HandoverBusinessNo { get; set; }

        /// <summary>
        /// 交接业务号
        /// </summary>
        [XmlElementAttribute("AKC561", IsNullable = false)]
        public decimal HandoverSign { get; set; }

    }
}
