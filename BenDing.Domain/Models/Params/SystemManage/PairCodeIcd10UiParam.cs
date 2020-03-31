using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Params.Base;

namespace BenDing.Domain.Models.Params.SystemManage
{
   public class PairCodeIcd10UiParam: GetHisBaseParam
    {
       
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
