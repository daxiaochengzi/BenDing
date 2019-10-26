using System;
using System.Collections.Generic;
using System.Text;
using BenDing.Domain.Models.Params.UI;

namespace BenDing.Domain.Models.Params.Web
{
   public class OrgParam: UiInIParam
    {/// <summary>
     /// 机构名称
     /// </summary>
        public string Name { get; set; }
    }
}
