   using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Workers
{
  public  class UserInfoIndustrialInjuryWorkersDto
    {/// <summary>
        /// 个人社会保障号  
        /// </summary>
        public string Po_grshbzh { get; set; }
        /// <summary>
        /// 姓名  
        /// </summary>
        public string Po_xm { get; set; }
        /// <summary>
        /// 性别  
        /// </summary>
        public string Po_xb { get; set; }
        /// <summary>
        /// 出生年月(yyyymmdd)  
        /// </summary>
        public string Po_csny { get; set; }
        /// <summary>
        /// 职工类别,分三位,前两位标示职工类别(见附表)第三位标示公务员类别(0非公务员,1公务员,2交费公务员,3公务员B)  
        /// </summary>
        public string Po_zglb { get; set; }
        /// <summary>
        /// 联系地址  
        /// </summary>
        public string Po_lxdz { get; set; }
        /// <summary>
        /// 联系电话  
        /// </summary>
        public string Po_lxdh { get; set; }
        /// <summary>
        /// 身份证号  
        /// </summary>
        public string Po_sfzh { get; set; }
        /// <summary>
        /// 实足年龄  
        /// </summary>
        public string Po_sznl { get; set; }
        /// <summary>
        /// 单位名称  
        /// </summary>
        public string Po_dwmc { get; set; }
        /// <summary>
        /// 参保状态  
        /// </summary>
        public string Po_cbzt { get; set; }
        /// <summary>
        /// 工伤住院报销特殊状态  1正常报销，2限制报销，3不能报销  
        /// </summary>
        public string Po_ybtszt { get; set; }
        /// <summary>
        /// 工伤报销状态说明  
        /// </summary>
        public string Po_ybbxztsm { get; set; }
    }
}
