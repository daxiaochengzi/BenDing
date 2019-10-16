using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Basiclevel.Param
{
   public class SaveMedicalInsuranceResidentInfoBasiclevelParam: HisBaseParam
    {/// <summary>
     /// id
     /// </summary>

        public string DataAllId { get; set; }
        /// <summary>
        /// 内容json
        /// </summary>
      
        public string ContentJson { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public string ResultDatajson { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 数据id
        /// </summary>
        public string DataId { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
        public string BusinessId { get; set; }
       
        /// <summary>
        /// 组织机构
        /// </summary>
      
        public string OrgCode { get; set; }
    }
}
