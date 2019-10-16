using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.DifferentPlaces
{
   public class DifferentPlacesMedicalRecordQueryDto:IniDto
    {
        public DifferentPlacesMedicalRecordQueryDataRow DataRow { get; set; }

    }

    public class DifferentPlacesMedicalRecordQueryDataRow
    {
        public List<DifferentPlacesMedicalRecordQueryRow>  Row { get; set; }

    }
    public class DifferentPlacesMedicalRecordQueryRow
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string akz007 { get; set; }
        /// <summary>
        /// 就诊登记号
        /// </summary>
        public string aaz217 { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string bkc131 { get; set; }
        /// <summary>
        /// 经办时间
        /// </summary>
        public string aae036 { get; set; }
        /// <summary>
        /// 病历图片
        /// </summary>
        public string tujpg { get; set; }
    }
}
