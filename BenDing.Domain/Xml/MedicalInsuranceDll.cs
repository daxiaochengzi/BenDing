using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BenDing.Domain.Xml
{
   public static class MedicalInsuranceDll
    {
        
        #region 居民医保 
        /// <summary>
        /// 链接服务器
        /// </summary>
        /// <param name="aLoginID"></param>
        /// <param name="aUserPwd"></param>
        /// <returns></returns>
        [DllImport("yyjk.dll", EntryPoint = "ConnectAppServer_cxjb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConnectAppServer_cxjb(String aLoginID, String aUserPwd);
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
        #region 异地 
        /// <summary>
        /// 链接服务器
        /// </summary>
        /// <param name="aLoginID"></param>
        /// <param name="aUserPwd"></param>
        /// <returns></returns>
        [DllImport("YBRSHisInterface.dll", EntryPoint = "ConnectAppServer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConnectAppServer(String aLoginID, String aUserPwd);
        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="aReaderPort">端口号</param>
        /// <param name="aCardPasswd">密码</param>
        /// <returns></returns>
        [DllImport("YBRSHisInterface.dll", EntryPoint = "ConnectAppServer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCardInfo(int aReaderPort, string aCardPasswd);
        #endregion
    }
}
