using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.DifferentPlaces
{
   public class DifferentPlacesPrescriptionUploadDto:IniDto
    {/// <summary>
     /// 处方明细上传时间
     /// </summary>
        public string po_jbsj { get; set; }

        public PrescriptionUploaddatarow DataRow { get; set; }
    } 

    public class PrescriptionUploaddatarow
    {

    }
    public class PrescriptionUploadrow
    {/// <summary>
     /// 费用明细序号
     /// </summary>
        public string bke019 { get; set; }
        /// <summary>
        /// 单条明细费用总额
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
        /// <summary>
        /// akc228
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
        /// 参保地限制使用标志
        /// </summary>
        public string cbdxzyyspbz { get; set; }

        
       
    }
}
