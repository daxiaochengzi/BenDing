using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentPlacesDepartmentInfoUploadParam
    {
        public int nums { get; set; }
        public DifferentPlacesDepartmentInfoUploadDataRow DataRow { get; set; }
    }

    public class DifferentPlacesDepartmentInfoUploadDataRow
    {
        public List<DifferentPlacesDepartmentInfoUploadRow>  Row { get; set; }
    }
    public class DifferentPlacesDepartmentInfoUploadRow
        {
       
        /// <summary>
        /// 医院科室编码（医院内部编码）
        /// </summary>
        public string aaz307 { get; set; }
        /// <summary>
        /// 医院科室名称(医院内部名称)
        /// </summary>
        public string bkc018 { get; set; }
        /// <summary>
        /// 科室类型
        /// </summary>
        public string ykf002 { get; set; }
        /// <summary>
        /// 科室代码
        /// </summary>
        public string akf001 { get; set; }
        /// <summary>
        /// 专业科室代码
        /// </summary>
        public string akf077 { get; set; }
        /// <summary>
        /// 科室负责人
        /// </summary>
        public string ykf003 { get; set; }
        /// <summary>
        /// 科室负责人联系电话
        /// </summary>
        public string ykf004 { get; set; }
        /// <summary>
        /// 科室成立时间
        /// </summary>
        public string ykf005 { get; set; }

        /// <summary>
        /// 批准床位数量
        /// </summary>
        public string ake020 { get; set; }
        /// <summary>
        /// 实际开放床位数量
        /// </summary>
        public string ake021 { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string aae030 { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string aae031 { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public string aae100 { get; set; }
    }
}
