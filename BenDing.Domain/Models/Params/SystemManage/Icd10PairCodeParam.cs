using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Web;

namespace BenDing.Domain.Models.Params.SystemManage
{
  public  class Icd10PairCodeParam
    {
        public  List<Icd10PairCodeDataParam>  DataList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UserInfoDto User { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        public string BusinessId { get; set; }
    }
    public class Icd10PairCodeDataParam
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
    }
}
