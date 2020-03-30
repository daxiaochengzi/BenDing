using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.SystemManage
{
   public class PairCodeIcd10UiParam
    {
        /// <summary>
        /// 操作人员id
        /// </summary>
        public string EmpId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>

        public string ProjectName { get; set; }
        /// <summary>
        /// 疾病id
        /// </summary>
        public string DiseaseId { get; set; }
    }
}
