using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Dto.Single
{/// <summary>
 /// 2.2.26.门诊诊查记录查询 单数
 /// </summary>
    public class OutpatientConsultationFeeQueryDto:IniDto
    {/// <summary>
    /// 
    /// </summary>
        public OutpatientConsultationFeeQueryRow PO_GHXX { get; set; }
    }
    /// <summary>
    ///  2.2.26.门诊诊查记录查询 复数
    /// </summary>
    public class OutpatientConsultationFeeQueryListDto : IniDto
    {
       public OutpatientConsultationFeeQueryRowList PO_GHXX { get; set; }
    }
    public class OutpatientConsultationFeeQueryRow
    {
        public OutpatientConsultationFeeQueryPO_GHXX ROW { get; set; }
    }
    public class OutpatientConsultationFeeQueryRowList
    {
        public   List<OutpatientConsultationFeeQueryPO_GHXX>  ROW { get; set; }
    }
    public class OutpatientConsultationFeeQueryPO_GHXX
    {/// <summary>
     /// 身份证号
     /// </summary>
        public string PO_AAC002 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string PO_AAC003 { get; set; }
        /// <summary>
        /// 结算人群
        /// </summary>
        public string PO_CKA549 { get; set; }
        /// <summary>
        /// 挂号医院
        /// </summary>
        public string PO_CKB519 { get; set; }
        /// <summary>
        /// 挂号费用
        /// </summary>
        public string PO_ZFY { get; set; }
        /// <summary>
        /// 挂号补助
        /// </summary>
        public string PO_GHBZ { get; set; }
        /// <summary>
        /// 患者支付金额
        /// </summary>
        public string PO_XJZF { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string PO_BZ { get; set; }


    }

}
