using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenDing.Domain.Models.Dto.Resident;

namespace BenDing.Domain.Xml
{/// <summary>
/// 
/// </summary>
    public static class CommonHelp
    {///编码
        public static string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        ///解码
        public static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        /// <summary>
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GuidToStr(string param)
        {
          return  BitConverter.ToInt64(Guid.Parse(param).ToByteArray(), 0).ToString();
        }
       
        /// <summary>
        /// 日志字符串格式化
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string FormatDateTime(string param)
        {
            return Convert.ToDateTime(param).ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 四舍五入到2位等同于Double
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static decimal ValueToDouble(decimal param)
        {
           return Math.Round(param, 1, MidpointRounding.AwayFromZero);


        }
        /// <summary>
        ///  字符串转换数值型
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static decimal ValueToDecimal(string param)
        {
            decimal resultData = 0;

            if (!string.IsNullOrWhiteSpace(param))
            {
                resultData = Convert.ToDecimal(param);
            }

            return resultData;
        }
        /// <summary>
        /// sql in串联
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ListToStr(List<string> param)
        {
            string result = null;
            if (param.Any())
            {
                foreach (var item in param)
                {
                    result += "'" + item + "'" + ",";
                }

            }
            return result?.Substring(0, result.Length - 1);
        }
    }

}
