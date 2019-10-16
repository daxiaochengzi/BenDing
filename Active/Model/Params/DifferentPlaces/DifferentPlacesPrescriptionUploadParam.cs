using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
  public  class DifferentPlacesPrescriptionUploadParam
    {
        
        /// <summary>
        /// 参保地统筹区代码
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 就诊记录号
        /// </summary>
        public string aaz217 { get; set; }
        /// <summary>
        /// 个人编码
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int nums { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string bkc131 { get; set; }
        /// <summary>
        /// 处方明细
        /// </summary>
        public Uploadcfmx CFMX { get; set; }

    }

    public class Uploadcfmx
    {
        public Uploaddatarow DATAROW { get; set; }
    }

    public class Uploaddatarow
    {
        public  List<Uploadrow> ROW { get; set; }
    }

    public class Uploadrow
    {
      
        /// <summary>
        /// 处方号(单据号)
        /// </summary>
        public string aae072 { get; set; }
        /// <summary>
        /// 医嘱记录序号
        /// </summary>
        public string bkc127 { get; set; }
        /// <summary>
        /// >序号（在医疗机构系统中产生的费用序号，一次就诊的序号不能重复）
        /// </summary>
        public string bke019 { get; set; }
        /// <summary>
        /// 全省统一收费目录编码
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
        /// 医保项目分类
        /// </summary>
        public string ake003 { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public double akc226 { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public double akc225 { get; set; }
        /// <summary>
        /// 费用总额
        /// </summary>
        public double akc264 { get; set; }
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
        /// <summary>
        /// 限制用药审批标志
        /// </summary>
        public string xzyyspbz { get; set; }
        
    }

}
