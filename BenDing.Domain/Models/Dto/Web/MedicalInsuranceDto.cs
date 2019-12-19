using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BenDing.Domain.Models.Dto.Web
{/// <summary>
/// 
/// </summary>
   public class MedicalInsuranceDto
        { /// <summary>
          /// Id
          /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 基层His住院Id
        /// </summary>
        public string BusinessId { get; set;}
        /// <summary>
        /// 医保住院号
        /// </summary>
        public string MedicalInsuranceHospitalizationNo { get; set; }
        /// <summary>
        /// 医保卡号
        /// </summary>
        public string InsuranceNo { get; set; }
       
     
        /// <summary>
        /// 入院信息
        /// </summary>
        public string AdmissionInfoJson { get; set; }
       
        /// <summary>
        /// 是否为修改
        /// </summary>
        public  bool IsModify { get; set; }
        /// <summary>
        /// 医保类别
        /// </summary>
        public  int InsuranceType { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>

        public  bool IsDelete { get; set; }=true;
    }
}
