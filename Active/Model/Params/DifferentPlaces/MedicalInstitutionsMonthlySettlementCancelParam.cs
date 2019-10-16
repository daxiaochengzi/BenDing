using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class MedicalInstitutionsMonthlySettlementCancelParam
    {
        //    <baa008>申请地统筹区编号</baa008>
        //<bkb019>医疗机构清算流水号</bkb019>
        //<bkc131>经办人姓名</bkc131>
        /// <summary>
        /// 申请地统筹区编号
        /// </summary>
        public string baa008 { get; set;  }
        /// <summary>
        /// 医疗机构清算流水号
        /// </summary>
        public string bkb019 { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string bkc131 { get; set; }
    }
}
