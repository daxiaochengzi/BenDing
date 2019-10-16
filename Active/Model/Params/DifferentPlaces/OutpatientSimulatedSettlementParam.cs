using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Model.Params.DifferentPlaces
{
   public class OutpatientSimulatedSettlementParam
    {
        /// <summary>
        /// 参保地统筹区编号
        /// </summary>
        public string baa008 { get; set; }
        /// <summary>
        /// 医院门诊号
        /// </summary>
        public string akc190 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string aac003 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string aac001 { get; set; }
        /// <summary>
        /// 社会保障卡卡号
        /// </summary>
        public string aaz500 { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string aac002 { get; set; }
        /// <summary>
        /// 医疗类别代码
        /// </summary>
        public string aka130 { get; set; }
        /// <summary>
        /// 清算分中心（医疗机构向市或者区（县）月结医疗费用
        /// </summary>
        public string bka013 { get; set; }
        /// <summary>
        /// 清算类别
        /// </summary>
        public string bka015 { get; set; }
        /// <summary>
        /// 门诊诊断编码1 门诊诊断的病种代码，见病种代码icd-10，医院门诊主诊断不能为空，药店购药可为空。
        /// </summary>
        public string akc193 { get; set; }
        /// <summary>
        /// 门诊诊断编码2
        /// </summary>
        public string bkc021 { get; set; }
        /// <summary>
        /// 门诊诊断编码3
        /// </summary>
        public string bkc022 { get; set; }
        /// <summary>
        /// 门诊诊断中文名称(医院来组织)
        /// </summary>
        public string bkc020 { get; set; }
        public OutpatientSimulatedSettlementDisease Disease { get; set; }
        public OutpatientSimulatedSettlementBkc033 Bkc033 { get; set; }
        /// <summary>
        /// 本次个人账户拟下账金额
        /// </summary>
        public double bkc142 { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string bkc131 { get; set; }
        /// <summary>
        /// 费用发生时间
        /// </summary>
        public string aae030 { get; set; }
        /// <summary>
        /// 明细数量
        /// </summary>

        public int nums { get; set; }
        public OutpatientSimulatedSettlementMzfymx Mzfymx { get; set; }

    }

    public class OutpatientSimulatedSettlementDisease
    {
        public  List<OutpatientSimulatedSettlementDiseaseRow>  Row { get; set; }
    }
    public class OutpatientSimulatedSettlementDiseaseRow
    {/// <summary>
     /// 特慢病病种编码（全省统一编码）
     /// </summary>
        public string bkc014 { get; set; }
    }
    public class OutpatientSimulatedSettlementBkc033
    {
        public List<OutpatientSimulatedSettlementBkc033Row> Row { get; set; }
    }
    public class OutpatientSimulatedSettlementBkc033Row
    {/// <summary>
     /// 入院次要诊断编码
     /// </summary>
        public string bkc022 { get; set; }
        /// <summary>
        /// 入院次要诊断名称
        /// </summary>
        public string akc076 { get; set; }

      

    }

    public class OutpatientSimulatedSettlementMzfymx
    {
        public OutpatientSimulatedSettlementDataRow DataRow { get; set; }
    }
    public class OutpatientSimulatedSettlementDataRow
    {
        public List<OutpatientSimulatedSettlementRow>  Row { get; set; }
    }
    public class OutpatientSimulatedSettlementRow
    {
        
        /// <summary>
        /// 费用明细序号
        /// </summary>
        public int bke019 { get; set; }
        /// <summary>
        /// 全省统一收费目录编码
        /// </summary>
        public string aaz231 { get; set; }
        /// <summary>
        /// 院内收费项目编码
        /// </summary>
        public string bke026 { get; set; }
        /// <summary>
        /// 院内收费项目名称
        /// </summary>
        public string bke027 { get; set; }
        /// <summary>
        /// 院内收费项目规格
        /// </summary>
        public string aka074_yn { get; set; }
        /// <summary>
        /// 院内收费项目剂型
        /// </summary>
        public string aka070_yn { get; set; }

        /// <summary>
        /// 本单收费单位
        /// </summary>
        public string aka067_yn { get; set; }
        /// <summary>
        /// 最小收费单位
        /// </summary>
        public string aka067 { get; set; }
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
        /// 处方医生编码
        /// </summary>
        public string bkc048 { get; set; }
        /// <summary>
        /// 处方医生姓名
        /// </summary>

        public string bkc049 { get; set; }
        
        /// <summary>
        /// 科室代码
        /// </summary>
        public string aaz307 { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string aae386 { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        public string bkc045 { get; set; }
        /// <summary>
        /// 每次用量
        /// </summary>
        public double bkc044 { get; set; }
        /// <summary>
        /// 与单次用量同单位规格
        /// </summary>
        public double aka074 { get; set; }
        /// <summary>
        /// 医保项目分类注：ake003为药品时项目不能为空；为非药品项目时aka074_yn、aka070_yn、bkc044、bkc045、aka074尽量填写，可以为空。
        /// </summary>
        public string ake003 { get; set; }
    }
}
