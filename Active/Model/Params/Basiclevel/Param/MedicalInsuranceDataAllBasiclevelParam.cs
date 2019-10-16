using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Basiclevel.Param
{
  public  class MedicalInsuranceDataAllBasiclevelParam
    {/// <summary>
        /// 机构编号
        /// </summary>
      
        public string OrgCode { get; set; }
        /// <summary>
        /// 数据id
        /// </summary>
     
        public string DataId { get; set; }
        /// <summary>
        /// 业务id
        /// </summary>
       

        public string BusinessId { get; set; }
        /// <summary>
        /// 数据类型交易码
        /// </summary>
        public string DataType { get; set; }
    }
}
