using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
  public  class DifferentPlacesMedicalRecordQueryParam
    {
        
        /// <summary>
        /// 查询地统筹区编码
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 文件ID
        /// </summary>
        public string akz007 { get; set; }
        /// <summary>
        /// 就诊登记号
        /// </summary>
        public string aaz217 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string aac002 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string aac003 { get; set; }
    }
}
