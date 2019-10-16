using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class DifferentPlacesDoctorInfoUploadParam
    {
        public int nums { get; set; }

        public DifferentPlacesDoctorInfoUploadDataRow DataRow { get; set; }
        //datarow
    }

    public class DifferentPlacesDoctorInfoUploadDataRow
    {
        public List<DifferentPlacesDoctorInfoUploadRow>  Row { get; set; }
    }
    public class DifferentPlacesDoctorInfoUploadRow
    {
        
        /// <summary>
        /// 医务人员ID（医院内部编码）
        /// </summary>
        public string aaz263 { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string aac002 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string aac003 { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string aac006 { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string aac011 { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string aac004 { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string aac005 { get; set; }
        /// <summary>
        /// 所属科室(医院内部编码)，需为yyxx01上传中的编码
        /// </summary>
        public string aaz307 { get; set; }
        /// <summary>
        /// 所学专业
        /// </summary>
        public string aac014 { get; set; }
        /// <summary>
        /// 发证机构
        /// </summary>
        public string yab029 { get; set; }
        /// <summary>
        /// 医务人员职务
        /// </summary>
        public string aaf009 { get; set; }
        /// <summary>
        /// 医务人员职称
        /// </summary>
        public string ykf011 { get; set; }
        /// <summary>
        /// 医务人员类别
        /// </summary>
        public string ykf012 { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string aae011 { get; set; }
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
