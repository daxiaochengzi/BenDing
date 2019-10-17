using BenDingWeb.Models.Params.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenDingWeb.Models.Params.UI
{
  public  class GetInpatientInfoDetailUIParam: BaseIniParam
    {
        public  List<string> IdCardList { get; set; }

    }
}
