using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{
  public  class MedicalDirectoryCodePairUiParam
    {/// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>

        public string ProjectName { get; set; }
        /// <summary>
        /// 项目编码类别
        /// </summary>
        public string ProjectCodeType { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
    }
}
