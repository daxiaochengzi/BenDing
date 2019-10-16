using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.DifferentPlaces
{
   public class DifferentPlacesHospitalizationExamineQueryDto:IniDto
    {
   
        /// <summary>
        /// 本次起付线
        /// </summary>
        public string bke002 { get; set; }
        /// <summary>
        /// 享受待遇标识
        /// </summary>
        public string bkc121 { get; set; }
        /// <summary>
        /// 不享受医保待遇原因
        /// </summary>
        public string bkc124 { get; set; }
    }
}
