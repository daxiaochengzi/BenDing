using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
  public  class DifferentPlacesMedicalRecordUploadParam
   {
        /// <summary>
        /// 参保地统筹区编码
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 就诊登记号
        /// </summary>
        public string aaz217 { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string bkc131 { get; set; }
        /// <summary>
        /// 经办时间
        /// </summary>
        public string aae036 { get; set; }
        /// <summary>
        /// 带路径图片文件名
        /// </summary>
        public string filename { get; set; }

    }
}
