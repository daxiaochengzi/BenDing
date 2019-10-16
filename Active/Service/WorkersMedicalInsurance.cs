using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Service
{/// <summary>
 /// 
 /// </summary>
    public static class WorkersMedicalInsurance
    {
        #region 职工医保 
      
        /// <summary>
        /// 获取个人基本资料
        /// </summary>
        /// <param name="pi_sfbz">个人IC卡号或身份证号或个人编号</param>
        /// <param name="pi_crbz">1为公民身份号码 2为医保卡号 3为个人编号</param>
        /// <param name="pi_xzqh">所属行政区</param>
        /// <param name="po_grshbzh">个人社会保障号</param>
        /// <param name="po_xm">姓名</param>
        /// <param name="po_xb">性别</param>
        /// <param name="po_csny">出生年月(yyyymmdd)</param>
        /// <param name="po_zglb">职工类别分三位,前两位标示职工类别(见附表)第三位标示公务员类别(0非公务员,1公务员,2交费公务员,3公务员B)</param>
        /// <param name="po_lxdz">联系地址</param>
        /// <param name="po_lxdh">联系电话</param>
        /// <param name="po_sfzh">身份证号</param>
        /// <param name="po_sznl">实足年龄</param>
        /// <param name="po_dwmc">单位名称</param>
        /// <param name="Po_cbzt">参保状态</param>
        /// <param name="po_grzhye">个人帐户余额</param>
        /// <param name="po_ybtszt">医保住院报销特殊状态  1正常报销，2限制报销，3不能报销</param>
        /// <param name="po_ybbxztsm">医保报销状态说明</param>
        /// <param name="Po_fhz">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="Po_msg">系统错误信息</param>
        [DllImport("yyjk.dll", EntryPoint = "hqgrjbzl", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int hqgrjbzl(string pi_sfbz, string pi_crbz, string pi_xzqh,
       StringBuilder po_grshbzh,StringBuilder po_xm,StringBuilder po_xb,StringBuilder po_csny,
       StringBuilder po_zglb,StringBuilder po_lxdz,StringBuilder po_lxdh,StringBuilder po_sfzh,
       StringBuilder po_sznl,StringBuilder po_dwmc,StringBuilder Po_cbzt,StringBuilder po_grzhye,
       StringBuilder po_ybtszt,StringBuilder po_ybbxztsm,StringBuilder Po_fhz,StringBuilder Po_msg
        );
        /// <summary>
        ///2.获取个人基本资料（工伤医疗）
        /// </summary>
        /// <param name="pi_sfbz">个人IC卡号或身份证号或个人编号</param>
        /// <param name="pi_crbz">1为公民身份号码 2为医保卡号 3为个人编号</param>
        /// <param name="pi_xzqh">所属行政区</param>
        /// <param name="po_grshbzh">个人社会保障号</param>
        /// <param name="po_xm">姓名</param>
        /// <param name="po_xb">性别</param>
        /// <param name="po_csny">出生年月(yyyymmdd)</param>
        /// <param name="po_zglb">职工类别分三位,前两位标示职工类别(见附表)第三位标示公务员类别(0非公务员,1公务员,2交费公务员,3公务员B)</param>
        /// <param name="po_lxdz">联系地址</param>
        /// <param name="po_lxdh">联系电话</param>
        /// <param name="po_sfzh">身份证号</param>
        /// <param name="po_sznl">实足年龄</param>
        /// <param name="po_dwmc">单位名称</param>
        /// <param name="po_cbzt">参保状态</param>
        /// <param name="po_ybtszt">医保住院报销特殊状态  1正常报销，2限制报销，3不能报销</param>
        /// <param name="po_ybbxztsm">医保报销状态说明</param>
        /// <param name="po_fhz">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="po_msg">系统错误信息</param>
        [DllImport("yyjk.dll", EntryPoint = "hqgrjbzl_gs", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int hqgrjbzl_gs(string pi_sfbz, string pi_crbz, string pi_xzqh,StringBuilder po_grshbzh,StringBuilder po_xm,
          StringBuilder po_xb,StringBuilder po_csny,StringBuilder po_zglb,
          StringBuilder po_lxdz,StringBuilder po_lxdh,StringBuilder po_sfzh,StringBuilder po_sznl,StringBuilder po_dwmc,
          StringBuilder po_cbzt,StringBuilder po_ybtszt,StringBuilder po_ybbxztsm,StringBuilder po_fhz,StringBuilder po_msg
         );
        /// <summary>
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
        public static extern int zydj(string pi_sfbz, string pi_crbz, string pi_xzqh, string pi_yybh, 
            string pi_yllb, string pi_ryrq, string pi_icd10, string pi_icd10_2, string pi_icd10_3, string pi_ryzd,
            string pi_zybq, string pi_cwh, string pi_yyzyh ,string pi_jbr,
          StringBuilder po_zyh,StringBuilder po_spbh,StringBuilder po_bnyzycs,StringBuilder po_bntcyzfje,StringBuilder po_bntckzfje,
          StringBuilder po_fhz,StringBuilder po_msg);
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
        public static extern int zyzlxgall(string pi_fwjgh, string  pi_zyh, string pi_xzqh, string pi_ryrq,
            string pi_icd10, string pi_icd10_2, string pi_icd10_3, string pi_ryzd, string pi_zybq, string pi_cwh,string pi_yyzyh ,
          StringBuilder po_fhz,StringBuilder po_msg);
        /// <summary>
        /// 住院费用计算
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
        public static extern int fyjs_new(string pi_fwjgh, string pi_zyh, string pi_xzqh, string pi_cszl, string pi_czy, string pi_cyrq,
            string pi_cyqk, string pi_icd10, string pi_icd10_2, string pi_icd10_3, string pi_cyzd,
          StringBuilder PO_FYZE,StringBuilder PO_TCZF,StringBuilder PO_BCZF,StringBuilder PO_ZXJJ,
          StringBuilder PO_GWYBT,StringBuilder PO_GWYBZ,StringBuilder PO_QTZF,StringBuilder PO_ZHZF,
          StringBuilder PO_XJZF,StringBuilder PO_qfje,StringBuilder PO_DJH ,StringBuilder po_bz,
          StringBuilder po_fhz,StringBuilder po_msg);
        /// <summary>
        /// 住院费用预计算
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
        public static extern int fyyjs_new(string pi_fwjgh, string pi_zyh, string pi_xzqh, string pi_cszl, string pi_czy,string pi_cyrq,
          StringBuilder PO_FYZE,StringBuilder PO_TCZF,
          StringBuilder po_bczf,StringBuilder po_zxjj,
          StringBuilder PO_GWYBT,StringBuilder PO_GWYBZ,
          StringBuilder Po_qtzf,StringBuilder PO_ZHZF,
          StringBuilder PO_XJZF,StringBuilder PO_qfje,
          StringBuilder PO_DJH,StringBuilder Po_bz,
          StringBuilder po_fhz,StringBuilder po_msg);
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
        public static extern int qxjs(string pi_xzqh, string pi_fwjgh, string pi_zyh, string pi_djh, string pi_qxcd, string pi_jbr,
          StringBuilder po_knbz,StringBuilder po_fhz,StringBuilder po_msg);
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
        public static extern int hqfyjsjg_new(string pi_fwjgh, string PI_ZYH, string pi_xzqh,
          StringBuilder PO_TCZF,  StringBuilder po_bczf,StringBuilder po_zxjj,  StringBuilder pO_GWYBT,
          StringBuilder PO_GWYBZ, StringBuilder po_qtzf,StringBuilder PO_ZHZF,  StringBuilder PO_XJZF,
          StringBuilder PO_QFJE,  StringBuilder PO_JSRQ,StringBuilder po_bz, StringBuilder PO_FHZ,
          StringBuilder PO_MSG);
        /// <summary>
        /// 处方项目传输
        /// </summary>
        /// <param name="pi_jzjlh">在医保上的住院号</param>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="po_pch">批次号(本次上传)</param>
        /// <param name="po_fhz">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="po_msg">系统错误信息</param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "xmlcfmxcs", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int xmlcfmxcs(string pi_jzjlh, string  pi_xzqh,
           StringBuilder po_pch,StringBuilder po_fhz,StringBuilder po_msg);
        /// <summary>
        /// 费用批次未注册确认信息查询
        /// </summary>
        /// <param name="pi_jzjlh">在医保上的住院号</param>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="po_pch">批次号(查询结果) 以逗号为分隔符分隔的未确认批次号字符串</param>
        /// <param name="po_fhz">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="po_msg">系统错误信息</param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "fypcxxcx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int fypcxxcx(string pi_jzjlh, string pi_xzqh,
          StringBuilder po_pch,StringBuilder po_fhz,StringBuilder po_msg);
        /// <summary>
        /// 费用批次未注册确认信息查询
        /// </summary>
        /// <param name="Pi_jzjlh">在医保上的住院号</param>
        /// <param name="Pi_xzqh">行政区划</param>
        /// <param name="Pi_pch">批次号(查询结果) </param>
        /// <param name="Pi_qrlx">确认类型 1确认，2取消</param>
        /// <param name="Pi_jbr">经办人</param>
        /// <param name="po_fhz">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="po_msg">系统错误信息</param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "fypcxxqr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int fypcxxqr(string Pi_jzjlh, string Pi_xzqh, string Pi_pch, string Pi_qrlx, string Pi_jbr,
          StringBuilder po_fhz,StringBuilder po_msg);
        /// <summary>
        /// 已经上传处方明细删除
        /// </summary>
        /// <param name="pi_jzjlh">在医保上的就诊记录号</param>
        /// <param name="pi_pch">批次号</param>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="po_fhz">过程返回值(为1时正常，否则不正常)</param>
        /// <param name="po_msg">系统错误信息</param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "cfmxplsc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int cfmxplsc(string pi_jzjlh, string  pi_pch, string  pi_xzqh,
          StringBuilder po_fhz,StringBuilder po_msg) ;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pi_jzjlh">在医保上的就诊记录号</param>
        /// <param name="pi_xzqh">行政区划</param>
        /// <param name="po_fhz"></param>
        /// <param name="po_msg"></param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "xmlycscfmxcx", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int xmlycscfmxcx(string pi_jzjlh, string pi_xzqh,
      StringBuilder po_fhz,StringBuilder po_msg);
        /// <summary>
        /// 14.项目字典下载
        /// </summary>
        /// <param name="pi_xmlb">项目类别： 药品：yp检查治疗项目：jc下载全部：all</param>
        /// <param name="po_fhz"></param>
        /// <param name="po_msg"></param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "xmzdxz", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int xmzdxz(string pi_xmlb,
          StringBuilder po_fhz,StringBuilder po_msg) ;
        /// <summary>
        /// 18.读持卡人基本信息
        /// </summary>
        /// <param name="pi_ReaderPort">读卡器所连接的端口</param>
        /// <param name="pi_CardPasswd">卡密码</param>
        /// <param name="po_dwmc">单位名称</param>
        /// <param name="po_Cardid">IC卡卡号</param>
        /// <param name="po_Sfzhm">公民身份证号码</param>
        /// <param name="po_Name">姓名</param>
        /// <param name="po_Sex">性别</param>
        /// <param name="po_Folk">民族</param>
        /// <param name="po_BirthPlace">出生地</param>
        /// <param name="po_BirthDate">出生日期</param>
        /// <param name="po_Acntbalance">个人帐户余额</param>
        /// <param name="po_fhz"></param>
        /// <param name="po_msg"></param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "ReadCardInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCardInfo(
            int pi_ReaderPort, string pi_CardPasswd,
          StringBuilder po_dwmc,StringBuilder po_Cardid,StringBuilder po_Sfzhm,
          StringBuilder po_Name,StringBuilder po_Sex,StringBuilder po_Folk,
          StringBuilder po_BirthPlace,StringBuilder po_BirthDate,StringBuilder po_Acntbalance,
          StringBuilder po_fhz,StringBuilder po_msg
            );
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
            int pi_ReaderPort, string pi_CardPasswd,string pi_fyze,
            string pi_hklb,string Pi_yybh,string pi_jbr,
          StringBuilder Po_hklsh,StringBuilder po_zhzfje,StringBuilder po_zfzfje,
          StringBuilder po_fhz,StringBuilder po_msg
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
        public static extern int xmljsxxcx(string pi_jsksrq, string  pi_jszzrq, string  pi_xzqh,
          StringBuilder po_fhz,StringBuilder po_msg) ;
        #endregion
        #region 居民医保 
        /// <summary>
        /// 链接服务器
        /// </summary>
        /// <param name="aLoginID"></param>
        /// <param name="aUserPwd"></param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "ConnectAppServer_cxjb" , CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConnectAppServer_cxjb( String aLoginID, String aUserPwd);

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
        #endregion

    }
}
