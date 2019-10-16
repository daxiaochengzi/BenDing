using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Xml
{
  public  class ValidXmlDto
    {/// <summary>
        /// 过程返回值(为1时正常，否则不正常)
        /// </summary>
        public string PO_FHZ { get; set; }
        /// <summary>
        /// 系统错误信息
        /// </summary>
        public string PO_MSG { get; set; }
        /// <summary>
        /// 判断返回结果是否多行结果
        /// </summary>
        public  bool IsRows { get; set; }
        
    }
}

