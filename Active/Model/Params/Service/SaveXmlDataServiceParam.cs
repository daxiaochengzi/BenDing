using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Service
{
  public  class SaveXmlDataServiceParam: HisBaseParam
    {

        public string Participation { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
       
        public string ResultData { get; set; }
        /// <summary>
        /// 医保返回的业务号
        /// </summary>
       
        public string BusinessNumber { get; set; }
    }
}
