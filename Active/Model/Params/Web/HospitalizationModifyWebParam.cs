using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Web
{
   public class HospitalizationModifyWebParam
    {
        /// <summary>
        /// 胎儿数   如果为生育入院（即就诊类别为71，72，41）的，需录入生育的胎儿数量
        /// </summary>
        public string PI_TES { get; set; }
        /// <summary>
        /// 户口性质如果为生育入院（即就诊类别为71，72，41）的，需录入产妇的户口性质。10:城镇户口20:农业户口
        /// </summary>
        public string PI_HKXZ { get; set; }

        /// <summary>
        /// 社保住院号 (来自数据库--MedicalInsuranceResidentInfo.ResultDataJson.PI_ZYH)
        /// </summary>
        public string PI_ZYH { get; set; }
        /// <summary>
        /// 医院住院号(来自数据库--MedicalInsuranceResidentInfo.ContentJson.PI_YYZYH)
        /// </summary>
        public string PI_YYZYH { get; set; } 
    }
}
