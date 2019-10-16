using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.Basiclevel
{
   public class QueryInpatientInfoBasicParam: HisBaseParam
    {
        /// <summary>
        /// 机构编号
        /// </summary>
        public string InstitutionalNumber { get; set; }
        /// <summary>
        /// 胎儿数   如果为生育入院（即就诊类别为71，72，41）的，需录入生育的胎儿数量
        /// </summary>
        public string PI_TES { get; set; }
        /// <summary>
        /// 户口性质如果为生育入院（即就诊类别为71，72，41）的，需录入产妇的户口性质。10:城镇户口20:农业户口
        /// </summary>
        public string PI_HKXZ { get; set; }
        /// <summary>
        /// 入院日期  (格式为YYYYMMDD)
        /// </summary>
        public string PI_RYRQ { get; set; }
        /// <summary>
        /// 11： 普通入院23:  市内转院住院14:  大病门诊15:  大病住院22:  急诊入院71： 顺产72： 剖宫产 41： 病理剖宫产
        /// </summary>
        public string PI_YLLB { get; set; }
        /// <summary>
        /// 1为公民身份号码 2为个人编号
        /// </summary>
        public string PI_CRBZ { get; set; }
        public  string PI_SFBZ { get; set; }

    }
}
