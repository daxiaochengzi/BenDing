using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using BenDing.Domain.Models.Dto.Base;
using BenDing.Domain.Models.Dto.Resident;
using BenDing.Domain.Models.Dto.Web;

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
            return BitConverter.ToInt64(Guid.Parse(param).ToByteArray(), 0).ToString();
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

        /// <summary>
        /// 获取类属性名称和描述信息的对照字典
        /// </summary>
        /// <param name="obj">实体类对象</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPropertyAliasDict(object obj)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Dictionary<string, string> propertyNameDict = GetPropertyFullNameDict(obj);
            StringBuilder sb = new StringBuilder();
            foreach (string simpleName in propertyNameDict.Keys)
            {
                string summary = GetPropertySummary(propertyNameDict[simpleName]);
                if (string.IsNullOrEmpty(summary))
                {
                    summary = simpleName;//如果找不到对象的名称，那么取其属性名称作为Summary信息
                }

                if (!dict.ContainsKey(simpleName))
                {
                    dict.Add(simpleName, summary);
                }
            }
            return dict;
        }
        /// <summary>
        /// 获取诊断字符串
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string DiagnosisStr(List<string> param)
        {
            string str = string.Join(",", param.ToArray());
            string resultData = str.Length > 20 ?string.Join(",", param.Take(2).ToArray()) : str;
            return resultData;
        }

        /// <summary>
        /// 获取类属性的名称和全称字典列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPropertyFullNameDict(object obj)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Type t = obj.GetType();

            PropertyInfo[] pi = t.GetProperties();
            foreach (PropertyInfo info in pi)
            {
                if (!dict.ContainsKey(info.Name))
                {
                    dict.Add(info.Name, string.Format("{0}.{1}", t.FullName, info.Name));
                }
            }

            return dict;
        }
        /// <summary>
        /// 根据类属性名称，获取对应的备注信息（如果键名不存在，返回空）
        /// </summary>
        /// <param name="classPropertyName">类全局名称（带命名空间）</param>
        /// <returns></returns>
        public static string GetPropertySummary(string classPropertyName)
        {
            var pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "bin\\BenDing.Domain.xml";
            if (!System.IO.File.Exists(pathXml))
            {
                throw new SystemException("BenDing.Domain.xml文件不存在!!!"+ pathXml);
            }
            string keyName = string.Format("//doc/members/member[@name='P:{0}']/summary", classPropertyName);

            XPathDocument doc = new XPathDocument(pathXml);
            XPathNavigator nav = doc.CreateNavigator();
            XPathNodeIterator iterator = nav.Select(keyName);

            string result = "";
            try
            {
                if (iterator.MoveNext())
                {
                    XPathNavigator nav2 = iterator.Current.Clone();
                    result += nav2.Value;
                }
            }
            catch (Exception ex)
            {
                ;
            }

            return result.Trim();
        }
        /// <summary>
        /// 诊断获取
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DiagnosisData GetDiagnosis(List<InpatientDiagnosisDto> param)
        {
            var resultData = new DiagnosisData();
            //主诊断
            var mainDiagnosisList = param.Where(c => c.IsMainDiagnosis = true)
                .Select(d => d.DiagnosisCode).Take(3).ToList();
            if (mainDiagnosisList.Any() == false) throw new Exception("主诊断不能为空!!!");
            resultData.AdmissionMainDiagnosisIcd10 = CommonHelp.DiagnosisStr(mainDiagnosisList);
            //第二诊断
            var nextDiagnosisList = param.Where(c => c.IsMainDiagnosis = false)
                .Select(d => d.DiagnosisCode).ToList();
            if (mainDiagnosisList.Any())
            {
                var diagnosisIcd10Two = nextDiagnosisList.Take(3).ToList();
                resultData.DiagnosisIcd10Two = CommonHelp.DiagnosisStr(diagnosisIcd10Two);
                if (nextDiagnosisList.Count > 3)
                {//第三诊断
                    resultData.DiagnosisIcd10Three = CommonHelp.DiagnosisStr(nextDiagnosisList.Where(d => !diagnosisIcd10Two.Contains(d)).Take(3).ToList());
                }
            }

            return resultData;
        }
    }

}
