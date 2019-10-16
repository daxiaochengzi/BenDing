using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto
{
   public class IniDto
    {   /// <summary>
        /// 过程返回值(为1时正常，否则不正常)
        /// </summary>
        public string PO_FHZ { get; set; }
        /// <summary>
        /// 系统错误信息
        /// </summary>
        public string PO_MSG { get; set; }

        public string JsonData { get; set; }
    }
}
