using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Repository.Providers.Web
{
   public class WorkerMedicalInsurance
    {  /// <summary>
     /// 住院登记
     /// </summary>
     /// <param name="pi_sfbz">个人IC卡号或身份证号</param>
     /// <param name="pi_crbz">为’1’表示卡号,’2’身份证号,3为个人编号</param>
     /// <param name="pi_xzqh">行政区划</param>
     /// <param name="pi_yybh">医院编号</param>
     /// <param name="pi_yllb">医疗类别(普通住院 21 ；工伤住院41 )</param>
     /// <param name="pi_ryrq">入院日期</param>
     /// <param name="pi_icd10">入院主要诊断疾病ICD-10编码</param>
     /// <param name="pi_icd10_2">入院诊断疾病ICD-10编码</param>
     /// <param name="pi_icd10_3">入院诊断疾病ICD-10编码</param>
     /// <param name="pi_ryzd">入院诊断</param>
     /// <param name="pi_jbr">经办人</param>
     /// <param name="po_zyh">社保住院号</param>
     /// <param name="po_spbh">审批编号</param>
     /// <param name="po_bnyzycs">本年已住院次数</param>
     /// <param name="po_bntcyzfje">本年统筹已支付金额</param>
     /// <param name="po_bntckzfje">本年统筹可支付金额</param>
     /// <param name="po_fhz">过程返回值(为1时正常，否则不正常)</param>
     /// <param name="po_msg">系统错误信息</param>
     /// <param name="pi_zybq">住院病区</param>
     /// <param name="pi_cwh">床位号</param>
     /// <param name="pi_yyzyh">住院号</param>
     /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "zydj", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int HospitalizationRegister(string pi_sfbz, string pi_crbz, string pi_xzqh, string pi_yybh,
            string pi_yllb, string pi_ryrq, string pi_icd10, string pi_icd10_2, string pi_icd10_3, string pi_ryzd,
            string pi_zybq, string pi_cwh, string pi_yyzyh, string pi_jbr,byte[] po_zyh, byte[] po_spbh, byte[] po_bnyzycs,
         byte[] po_bntcyzfje,  byte[] po_bntckzfje,byte[] po_fhz, byte[] po_msg);
        /// <summary>
        /// 4.住院资料全部修改
        /// </summary>
        /// <param name="pi_fwjgh">医疗机构号</param>
        /// <param name="pi_zyh">住院号</param>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="pi_ryrq">入院日期</param>
        /// <param name="pi_icd10">入院主要诊断疾病ICD-10编码</param>
        /// <param name="pi_icd10_2">入院诊断疾病ICD-10编码</param>
        /// <param name="pi_icd10_3">入院诊断疾病ICD-10编码</param>
        /// <param name="pi_ryzd">入院诊断</param>
        /// <param name="pi_zybq">住院病区</param>
        /// <param name="pi_cwh">床位号</param>
        /// <param name="pi_yyzyh">医院住院号</param>
        /// <param name="po_fhz">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="po_msg">系统错误信息</param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "zyzlxgall", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ModifyHospitalization(string pi_fwjgh, string pi_zyh, string pi_xzqh, string pi_ryrq,
            string pi_icd10, string pi_icd10_2, string pi_icd10_3, string pi_ryzd, string pi_zybq, string pi_cwh, string pi_yyzyh,
          byte[] po_fhz, byte[] po_msg);
        /// <summary>
        /// 住院费用结算
        /// </summary>
        /// <param name="pi_fwjgh">医疗机构号</param>
        /// <param name="pi_zyh">住院号</param>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="pi_cszl">是否计住院次数</param>
        /// <param name="pi_czy">操作员</param>
        /// <param name="pi_cyrq">出院日期</param>
        /// <param name="pi_cyqk">出院情况（1康复，2转院，3死亡，4其他）</param>
        /// <param name="pi_icd10">出院主要诊断疾病ICD-10编码</param>
        /// <param name="pi_icd10_2">出院诊断疾病ICD-10编码</param>
        /// <param name="pi_icd10_3">出院诊断疾病ICD-10编码</param>
        /// <param name="pi_cyzd">出院诊断（确诊疾病）</param>
        /// <param name="PO_FYZE">发生费用金额</param>
        /// <param name="PO_TCZF">基本统筹支付</param>
        /// <param name="PO_BCZF">补充医疗保险支付金额</param>
        /// <param name="PO_ZXJJ">专项基金支付金额</param>
        /// <param name="PO_GWYBT">公务员补贴</param>
        /// <param name="PO_GWYBZ">公务员补助</param>
        /// <param name="PO_QTZF">其它支付金额</param>
        /// <param name="PO_ZHZF">帐户支付</param>
        /// <param name="PO_XJZF">现金支付</param>
        /// <param name="PO_qfje">起付金额</param>
        /// <param name="PO_DJH">单据号</param>
        /// <param name="po_bz">备注</param>
        /// <param name="po_fhz">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="po_msg">系统错误信息</param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "fyjs_new", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int WorkerHospitalizationSettlement(string pi_fwjgh, string pi_zyh, string pi_xzqh, string pi_cszl, string pi_czy, string pi_cyrq,
            string pi_cyqk, string pi_icd10, string pi_icd10_2, string pi_icd10_3, string pi_cyzd,
          byte[] PO_FYZE, byte[] PO_TCZF, byte[] PO_BCZF, byte[] PO_ZXJJ,
          byte[] PO_GWYBT, byte[] PO_GWYBZ, byte[] PO_QTZF, byte[] PO_ZHZF,
          byte[] PO_XJZF, byte[] PO_qfje, byte[] PO_DJH, byte[] po_bz,
          byte[] po_fhz, byte[] po_msg);
        /// <summary>
        /// 住院费用预结算
        /// </summary>
        /// <param name="pi_fwjgh">医疗机构号</param>
        /// <param name="pi_zyh">住院号</param>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="pi_cszl">是否计住院次数</param>
        /// <param name="pi_czy">操作员</param>
        /// <param name="pi_cyrq">出院日期</param>
        /// <param name="PO_FYZE">发生费用金额</param>
        /// <param name="PO_TCZF">基本统筹支付</param>
        /// <param name="po_bczf">补充医疗保险支付金额</param>
        /// <param name="po_zxjj">专项基金支付金额</param>
        /// <param name="PO_GWYBT">公务员补贴</param>
        /// <param name="PO_GWYBZ">公务员补助</param>
        /// <param name="Po_qtzf">其它支付金额</param>
        /// <param name="PO_ZHZF">帐户支付</param>
        /// <param name="PO_XJZF">现金支付</param>
        /// <param name="PO_qfje">起付金额</param>
        /// <param name="PO_DJH">单据号</param>
        /// <param name="Po_bz">备注</param>
        /// <param name="po_fhz"></param>
        /// <param name="po_msg"></param>
        [DllImport("yyjk.dll", EntryPoint = "fyyjs_new", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int WorkerHospitalizationPreSettlement(string pi_fwjgh, string pi_zyh, string pi_xzqh, string pi_cszl, string pi_czy, string pi_cyrq,
          byte[] PO_FYZE, byte[] PO_TCZF,
          byte[] po_bczf, byte[] po_zxjj,
          byte[] PO_GWYBT, byte[] PO_GWYBZ,
          byte[] Po_qtzf, byte[] PO_ZHZF,
          byte[] PO_XJZF, byte[] PO_qfje,
          byte[] PO_DJH, byte[] Po_bz,
          byte[] po_fhz, byte[] po_msg);
        /// <summary>
        /// 结算取消
        /// </summary>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="pi_fwjgh">医疗机构号</param>
        /// <param name="pi_zyh">住院号</param>
        /// <param name="pi_djh">登记号</param>
        /// <param name="pi_qxcd">取消程度(1取消结算2删除资料)</param>
        /// <param name="pi_jbr">经办人</param>
        /// <param name="po_knbz">跨年标志</param>
        /// <param name="po_fhz">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="po_msg">系统错误信息</param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "qxjs", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int WorkerSettlementCancel(string pi_xzqh, string pi_fwjgh, string pi_zyh, string pi_djh, string pi_qxcd, string pi_jbr,
          byte[] po_knbz, byte[] po_fhz, byte[] po_msg);
        /// <summary>
        /// 查询费用结算结果
        /// </summary>
        /// <param name="pi_fwjgh">医疗机构号</param>
        /// <param name="PI_ZYH">住院号</param>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="PO_TCZF">基本医疗统筹支付</param>
        /// <param name="po_bczf">补充医疗保险支付金额</param>
        /// <param name="po_zxjj">专项基金支付金额</param>
        /// <param name="pO_GWYBT">公务员补贴</param>
        /// <param name="PO_GWYBZ">公务员补贴</param>
        /// <param name="po_qtzf">其它支付金额</param>
        /// <param name="PO_ZHZF">帐户支付</param>
        /// <param name="PO_XJZF">现金支付</param>
        /// <param name="PO_QFJE">起付金额</param>
        /// <param name="PO_JSRQ">结算日期</param>
        /// <param name="po_bz">备注</param>
        /// <param name="PO_FHZ">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="PO_MSG">系统错误信息</param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "hqfyjsjg_new", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int QueryWorkerHospitalSettlement(string pi_fwjgh, string PI_ZYH, string pi_xzqh,
          byte[] PO_TCZF, byte[] po_bczf, byte[] po_zxjj, byte[] pO_GWYBT,
          byte[] PO_GWYBZ, byte[] po_qtzf, byte[] PO_ZHZF, byte[] PO_XJZF,
          byte[] PO_QFJE, byte[] PO_JSRQ, byte[] po_bz, byte[] PO_FHZ,
          byte[] PO_MSG);
        /// <summary>
        /// 业务功能调用
        /// </summary>
        /// <param name="aFuncCode"></param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "CallService_cxjb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallService_cxjb(string aFuncCode);
        /// <summary>
        /// 读取社保卡
        /// </summary>
        /// <param name="aReaderPort"></param>
        /// <param name="aCardPasswd"></param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "ReadCardInfo_cxjb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCardInfo_cxjb(string aReaderPort, string aCardPasswd);
        /// <summary>
        /// 19.IC卡：IC卡划卡操作
        /// </summary>
        /// <param name="pi_ReaderPort">读卡器所连接的端口</param>
        /// <param name="pi_CardPasswd">卡密码</param>
        /// <param name="pi_fyze">消费费用总额</param>
        /// <param name="pi_hklb">划卡类别@1门诊划卡2住院划卡</param>
        /// <param name="Pi_yybh">医院编号</param>
        /// <param name="pi_jbr">经办人</param>
        /// <param name="Po_hklsh">帐户支付金额</param>
        /// <param name="po_zhzfje">自费支付金额</param>
        /// <param name="po_fhz"></param>
        /// <param name="po_msg"></param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "hkgl", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int WorkerHospitalSettlement(
            int pi_ReaderPort, string pi_CardPasswd, string pi_fyze,
            string pi_hklb, string pi_yybh, string pi_jbr,
          byte[] po_hklsh, byte[] po_zhzfje, byte[] po_zfzfje,
          byte[] po_fhz, byte[] po_msg
            );
        /// <summary>
        /// 已医疗机构结算信息查询
        /// </summary>
        /// <param name="pi_jsksrq">结算开始日期（YYYYMMDD）</param>
        /// <param name="pi_jszzrq">结算终止日期（YYYYMMDD）</param>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="po_fhz"></param>
        /// <param name="po_msg"></param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "xmljsxxcx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int xmljsxxcx(string pi_jsksrq, string pi_jszzrq, string pi_xzqh,
          byte[] po_fhz, byte[] po_msg);
    
    }
}
