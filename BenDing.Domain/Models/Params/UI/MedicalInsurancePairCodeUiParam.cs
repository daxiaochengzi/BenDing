using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Models.Params.UI
{
   public class MedicalInsurancePairCodeUiParam
    {
        /// <summary>
        /// 目录类型
        /// </summary>
        public  string DirectoryType { get; set; }
       /// <summary>
       /// 医保项目编号
       /// </summary>
       public  string MedicalInsuranceDirectoryCode { get; set; }
        /// <summary>
        /// his项目编号
        /// </summary>
        public string HisDirectoryCode { get; set; }
        /// <summary>
        /// 对码Id
        /// </summary>
        public  Guid? PairCodeId { get; set; }
    }
}
