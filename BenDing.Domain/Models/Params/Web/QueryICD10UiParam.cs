using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Base;

namespace BenDing.Domain.Models.Params.Web
{
   public class QueryICD10UiParam: PaginationDto
    {/// <summary>
     /// 查询关键字
     /// </summary>
        public string Search { get; set; }
        /// <summary>
        /// 疾病名称
        /// </summary>
        public string DiseaseName{ get; set; }
        /// <summary>
        /// 疾病编码
        /// </summary>
        public string DiseaseCoding { get; set; }
        /// <summary>
        /// 医保编码
        /// </summary>

        public string ProjectCode { get; set; }
        /// <summary>
        /// 医保编码
        /// </summary>

        public string ProjectName { get; set; }
        /// <summary>
        /// 是否医保icd10
        /// </summary>
        public int IsMedicalInsurance { get; set; }

      


    }
}
