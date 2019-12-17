using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Repository.Providers.Web
{
   public class WorkerMedicalInsurance
    {/// <summary>
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
        [DllImport("yyjk.dll", EntryPoint = "zydj", CharSet = CharSet.Ansi, PreserveSig = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int HospitalizationRegister(string pi_sfbz, string pi_crbz, string pi_xzqh, string pi_yybh,
            string pi_yllb, string pi_ryrq, string pi_icd10, string pi_icd10_2, string pi_icd10_3, string pi_ryzd,
            string pi_zybq, string pi_cwh, string pi_yyzyh, string pi_jbr,
         [MarshalAs(UnmanagedType.LPStr)]StringBuilder po_zyh, [MarshalAs(UnmanagedType.LPStr)]StringBuilder po_spbh, [MarshalAs(UnmanagedType.LPStr)]StringBuilder po_bnyzycs,
         [MarshalAs(UnmanagedType.LPStr)]StringBuilder po_bntcyzfje, [MarshalAs(UnmanagedType.LPStr)] StringBuilder po_bntckzfje,
         [MarshalAs(UnmanagedType.LPStr)]StringBuilder po_fhz, [MarshalAs(UnmanagedType.LPStr)]StringBuilder po_msg);
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
        public static extern int Hospitalizationmodify(string pi_fwjgh, string pi_zyh, string pi_xzqh, string pi_ryrq,
            string pi_icd10, string pi_icd10_2, string pi_icd10_3, string pi_ryzd, string pi_zybq, string pi_cwh, string pi_yyzyh,
          StringBuilder po_fhz, StringBuilder po_msg);

        /// <summary>
        /// 断开服务器
        /// </summary>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "DisConnectAppServer_cxjb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int DisConnectAppServer_cxjb();
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
        public static extern int hkgl(
            int pi_ReaderPort, string pi_CardPasswd, string pi_fyze,
            string pi_hklb, string pi_yybh, string pi_jbr,
          StringBuilder po_hklsh, StringBuilder po_zhzfje, StringBuilder po_zfzfje,
          StringBuilder po_fhz, StringBuilder po_msg
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
          StringBuilder po_fhz, StringBuilder po_msg);
    
    }
}
