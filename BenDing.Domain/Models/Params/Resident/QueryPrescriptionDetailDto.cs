using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BenDing.Domain.Models.Params.Resident
{/// <summary>
 /// 处方明细查询
 /// </summary>
    [XmlRootAttribute("CFMX", IsNullable = false)]
    public class QueryPrescriptionDetailDto
    {

        [XmlElementAttribute("ROW")]
      
        public List<QueryPrescriptionDetailRowDto> RowDataList { get; set; }
       }

    }
  
   
    /// <summary>
    /// 
    /// </summary>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class QueryPrescriptionDetailRowDto {
        /// <summary>
        /// 行号
        /// </summary>
        [XmlElementAttribute("CO", IsNullable = false)]
      
        public int ColNum { get; set; }
        /// <summary>
        /// 医院编号
        /// </summary>
        [XmlElementAttribute("AKB020", IsNullable = false)]
     
        public string HospitalNumber { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        [XmlElementAttribute("AKC190", IsNullable = false)]
       
        public string HospitalizationNo { get; set; }

        /// <summary>
        /// 处方号 len(20)
        /// </summary>
        [XmlElementAttribute("AKC220", IsNullable = false)]
        
        public string PrescriptionNum { get; set; }
        /// <summary>
        ///  处方序号
        /// </summary>
        [XmlElementAttribute("AKC584", IsNullable = false)]
       
        public int PrescriptionSort { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        [XmlElementAttribute("AKC222", IsNullable = false)]
       
        public string ProjectCode { get; set; }
        /// <summary>
        /// 院内目录固定编码 len(20)
        /// </summary>
        [XmlElementAttribute("AKC515", IsNullable = false)]
        
        public string FixedEncoding { get; set; }
        /// <summary>
        /// 开处方日期 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        [XmlElementAttribute("AKC221", IsNullable = false)]
       
        public string DirectoryDate { get; set; }
        /// <summary>
        /// 结算处方日期 (yyyy-MM-dd HH:mm:ss)
        /// </summary>
        [XmlElementAttribute("AAE040", IsNullable = false)]
        
        public string DirectorySettlementDate { get; set; }
        /// <summary>
        /// 收费类别
        /// </summary>
        [XmlElementAttribute("AKA063", IsNullable = false)]
      
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [XmlElementAttribute("AKC223", IsNullable = false)]
       
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目级别
        /// </summary>
        [XmlElementAttribute("AKA065", IsNullable = false)]
      

        public string ProjectLevel { get; set; }
        /// <summary>
        /// 单价   (12,4)
        /// </summary>
        [XmlElementAttribute("AKC225", IsNullable = false)]
        
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 数量   (12,4)
        /// </summary>
        [XmlElementAttribute("AKC226", IsNullable = false)]
       
        public decimal Quantity { get; set; }

        /// <summary>
        /// 金额  (12,4)每条费用明细的数据校验为传入的金额（四舍五入到两位小数）和传入的单价*传入的数量（四舍五入到两位小数）必须相等，检查不等的会提示报错
        /// </summary>
        [XmlElementAttribute("AKC227", IsNullable = false)]
       
        public decimal Amount { get; set; }
        /// <summary>
        /// 居民自付金额 (12,2)
        /// </summary>
        [XmlElementAttribute("AKC228", IsNullable = false)]
       
        public decimal ResidentSelfPayProportion { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        [XmlElementAttribute("AKA070", IsNullable = false)]
        
        public string Formulation { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        [XmlElementAttribute("AKA071", IsNullable = false)]
       
        public decimal Dosage { get; set; }

        /// <summary>
        /// 使用频率
        /// </summary>
        [XmlElementAttribute("AKA072", IsNullable = false)]
        
        public string UseFrequency { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        [XmlElementAttribute("AKA073", IsNullable = false)]
        
        public string Usage { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [XmlElementAttribute("AKA074", IsNullable = false)]
       
        public string Specification { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [XmlElementAttribute("AKA067", IsNullable = false)]
       
        public string Unit { get; set; }
        /// <summary>
        /// 执行天数
        /// </summary>
        [XmlElementAttribute("AKC229", IsNullable = false)]
      
        public int UseDays { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [XmlElementAttribute("AAE013", IsNullable = false)]
       
        public string Remark { get; set; }
       
    }

