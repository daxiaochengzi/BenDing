using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Dto.Web
{
   public class ICD10PairCodeDto
    { 
        /// <summary>
        /// 基层疾病id
        /// </summary>
        public string DiseaseId { get; set; }
        /// <summary>
        /// 医保疾病编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 医保疾病编码
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PairCodeUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreateTime { get; set; }

    }
}
