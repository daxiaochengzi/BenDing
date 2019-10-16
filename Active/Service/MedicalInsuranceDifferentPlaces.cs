using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Service
{/// <summary>
/// 异地医保接口dll
/// </summary>
  public static  class MedicalInsuranceDifferentPlaces
    {
        /// <summary>
        /// 登陆连接
        /// </summary>
        /// <param name="aLoginId"></param>
        /// <param name="aUserPwd"></param>
        /// <returns></returns>
        [DllImport("YBRSHisInterface.dll", EntryPoint = " ConnectAppServer", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ConnectAppServer(string aLoginId,string aUserPwd);
        /// <summary>
        /// 读卡获取居民基本资料
        /// </summary>
        /// <param name="aReaderPort"></param>
        /// <param name="aUserPwd"></param>
        /// <returns></returns>
        [DllImport("YBRSHisInterface.dll", EntryPoint = " ReadCardInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadCardInfo(string aReaderPort, string aUserPwd);
        /// <summary>
        /// 所有功能交易
        /// </summary>
        /// <param name="functionCoding">功能编码</param>
        /// <returns></returns>
        [DllImport("YBRSHisInterface.dll", EntryPoint = " yyjk_call", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int yyjk_call(string functionCoding);

    }
}
