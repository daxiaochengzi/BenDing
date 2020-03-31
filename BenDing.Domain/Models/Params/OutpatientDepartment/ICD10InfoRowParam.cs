using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{
  public  class ICD10InfoRowParam
    {
        public string 验证码 { get; set; }
        public string 病种名称 { get; set; }
        public string 开始时间 { get; set; }
        public string 结束时间 { get; set; }
        /// <summary>
        /// 0西医诊断、1中医诊断、空或NULL全部（默认为空）
        /// </summary>
        public int 疾病类别 { get; set; }
    }
}
