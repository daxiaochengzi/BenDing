using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentHospitalizationModifyParam
    {/// <summary>
     /// 就诊记录号
     /// </summary>
        public string aaz217 { get; set; }

        /// <summary>
        /// 个人编号
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string aac002 { get; set; }
        /// <summary>
        /// 社保卡号
        /// </summary>
        public string aaz500 { get; set; }
        /// <summary>
        /// 住院类型
        /// </summary>
        public string aka042 { get; set; }
        /// <summary>
        /// 参保地统筹编码
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 住院科室
        /// </summary>
        public string bkc018 { get; set; }
        /// <summary>
        /// 所属科室，需为yyxx01上传中的编码
        /// </summary>
        public string aaz307 { get; set; }
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
        /// 病历号
        /// </summary>
        public string bkc015 { get; set; }
        /// <summary>
        /// 住院号
        /// </summary>
        public string akc190 { get; set; }
        /// <summary>
        /// 入院时间
        /// </summary>
        public string aae030 { get; set; }
      
       
        /// <summary>
        /// 入院床位号
        /// </summary>
        public string bkc019 { get; set; }
        /// <summary>
        /// 入院诊断医生编码
        /// </summary>
        public string ake022_bm { get; set; }
        /// <summary>
        /// 入院诊断医生名称
        /// </summary>
        public string ake022 { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string bkc017 { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string aae004 { get; set; }
        /// <summary>
        /// 联系电话 
        /// </summary>
        public string aae005 { get; set; }
    }
}
