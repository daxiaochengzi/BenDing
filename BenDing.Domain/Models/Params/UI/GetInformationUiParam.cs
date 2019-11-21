using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{/// <summary>
/// 获取基础信息
/// </summary>
  public class GetInformationUiParam: UiInIParam
    { /// <summary>
      /// 目录类型
      /// </summary>
        public string DirectoryType { get; set; }
    }
}
