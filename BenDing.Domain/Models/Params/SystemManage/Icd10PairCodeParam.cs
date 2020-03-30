using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.SystemManage
{
  public  class Icd10PairCodeParam
    {/// <summary>
    /// 基层疾病id
    /// </summary>
        public string DiseaseId { get; set; }
        /// <summary>
        /// 医保编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 基层名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserInfoDto User { get; set; }
    }
}
