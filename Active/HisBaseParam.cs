using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model
{
  public  class HisBaseParam
    {/// <summary>
     /// 医保医院编号
     /// </summary>
        public string YbOrgCode { get; set; }
        /// <summary>
        /// 医院ID
        /// </summary>
        public string OrgID { get; set; }
        /// <summary>
        /// 门诊或住院业务ID
        /// </summary>
        public string BID { get; set; }
        /// <summary>
        /// 操作人员ID
        /// </summary>
        public string EmpID { get; set; }
        /// <summary>
        /// 医保交易码
        /// </summary>
        public string BsCode { get; set; }
        /// <summary>
        /// HIS调用医保交易动作产生的唯一ID
        /// </summary>
        public string TransKey { get; set; }
        /// <summary>
        /// 医生编号
        /// </summary>
        public string EmpIDCode { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
    }
}
