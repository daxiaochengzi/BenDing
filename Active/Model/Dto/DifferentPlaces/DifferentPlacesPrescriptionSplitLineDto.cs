using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.DifferentPlaces
{
  public  class DifferentPlacesPrescriptionSplitLineDto:IniDto
    {
       
        /// <summary>
        /// 序号
        /// </summary>
        public string bke019 { get; set; }
        /// <summary>
        /// 全省统一目录编码
        /// </summary>
        public string aaz231 { get; set; }
        /// <summary>
        /// 医院内项目编码
        /// </summary>
        public string bke026 { get; set; }
        /// <summary>
        /// 医院内项目名称(诊疗项目)
        /// </summary>
        public string bke027 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public string akc226 { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string akc225 { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public string akc264 { get; set; }
        /// <summary>
        /// 定价上限金额
        /// </summary>
        public string aka068 { get; set; }
        /// <summary>
        /// 自费比例
        /// </summary>
        public string aka057 { get; set; }
        /// <summary>
        /// 全自费部分
        /// </summary>
        public string akc253 { get; set; }


        //<akc228>先自付部分</akc228>
        //<akc268>超限自付部分</akc268>
        //<bkc042>进入报销范围部分</bkc042>
        //<aka065>收费项目等级</aka065>
        //<aka067>最小收费单位</aka067>
        //<aka074_yn>医院内规格</aka074_yn>
        //<aka070_yn>医院内剂型</aka070_yn>
        /// <summary>
        /// 先自付部分
        /// </summary>
        public string akc228 { get; set; }
        /// <summary>
        /// 超限自付部分
        /// </summary>
        public string akc268 { get; set; }
        /// <summary>
        /// 进入报销范围部分
        /// </summary>
        public string bkc042 { get; set; }
        /// <summary>
        /// 收费项目等级
        /// </summary>
        public string aka065 { get; set; }
        /// <summary>
        /// 最小收费单位
        /// </summary>
        public string aka067 { get; set; }
        /// <summary>
        /// 医院内规格
        /// </summary>
        public string aka074_yn { get; set; }
        /// <summary>
        /// 医院内剂型
        /// </summary>
        public string aka070_yn { get; set; }
        /// <summary>
        /// 每次用量
        /// </summary>
        public string bkc044 { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        public string bkc045 { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string bkc046 { get; set; }
        /// <summary>
        /// 开单医生编码
        /// </summary>
        public string bkc048 { get; set; }
        /// <summary>
        /// 开单医生姓名
        /// </summary>
        public string bkc049 { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string aae386 { get; set; }
        /// <summary>
        /// 收费时间（开单日期）
        /// </summary>
        public string bkc040 { get; set; }
      
    }
}
