using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Web;
using Newtonsoft.Json;

namespace BenDing.Domain.Models.Params.OutpatientDepartment
{ /// <summary>
  /// 门诊结算取消
  /// </summary>
    [XmlRoot("ROW", IsNullable = false)]
    public class CancelOutpatientDepartmentCostParam
    {
        [XmlElementAttribute("PI_BAE007", IsNullable = false)]
        public string DocumentNo { get; set; }
    }

}
