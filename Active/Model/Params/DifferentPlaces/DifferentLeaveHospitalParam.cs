using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentLeaveHospitalParam
       {/// <summary>
         /// 参保地行政区划代码
         /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 就诊记录号
        /// </summary>
        public string aaz217 { get; set; }

        /// <summary>
        /// 个人编号
        /// </summary>
        public string aac001 { get; set; }
    
        /// <summary>
        /// 出院病历号
        /// </summary>

        public string bkc015 { get; set; }
        /// <summary>
        /// 出院住院号
        /// </summary>
        public string akc190 { get; set; }
        /// <summary>
        /// 出院原因1好转、2康复、3转院、4死亡、5其它
        /// </summary>
        public string bkc031 { get; set; }
        /// <summary>
        /// 出院时间
        /// </summary>
        public string aae031 { get; set; }


        /// <summary>
        /// 主疾病诊断编码
        /// </summary>
        public string akc193 { get; set; }
        /// <summary>
        /// 入院诊断编码2
        /// </summary>
        public string bkc021 { get; set; }
        /// <summary>
        /// 入院诊断编码3
        /// </summary>
        public string bkc022 { get; set; }
        /// <summary>
        /// 入院诊断中文名
        /// </summary>
        public string bkc020 { get; set; }
        public List<bkc033> bkc033 { get; set; }
        /// <summary>
        /// 出院诊断编码1
        /// </summary>
        public string akc196 { get; set; }
        /// <summary>
        /// 出院诊断编码2
        /// </summary>
        public string bkc028 { get; set; }
        /// <summary>
        /// 出院诊断编码3
        /// </summary>
        public string bkc029 { get; set; }
        /// <summary>
        /// 出院诊断中文名
        /// </summary>
        public string bkc027 { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string bkc025 { get; set; }
        /// <summary>
        /// 出院床位
        /// </summary>
        public string bkc026 { get; set; }
        /// <summary>
        /// 出院诊断医生编码
        /// </summary>
        public string ake021_bm { get; set; }
        /// <summary>
        /// 出院诊断医生
        /// </summary>
        public string ake021 { get; set; }
        /// <summary>
        /// 出院经办人
        /// </summary>
        public string bkc024 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string aae013 { get; set; }
    }
}
