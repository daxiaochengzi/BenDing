﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
    {/// <summary>
    /// 
    /// </summary>   
    /// <returns></returns>
        public static string GetWebServiceUrl()
        {
            string resultData = null;
            //测试a
           // resultData = "http://47.111.29.88:11013/WebService.asmx";
            //正式
            resultData = "http://11.21.1.11:8002/WebService.asmx";
            return resultData;
        }

        //编码
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
        public static string DiagnosisStr(List<InpatientDiagnosisDto> param)
        {
            var dataList = param.Select(c => c.DiseaseCoding).ToList();
            string str = string.Join(",", dataList.ToArray());
            string resultData = str.Length > 20 ? string.Join(",", dataList.Take(2).ToArray()) : str;
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
                throw new SystemException("BenDing.Domain.xml文件不存在!!!" + pathXml);
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


            //判断医保诊断不能为空

            var emptyData = param.Where(c => c.ProjectCode == null).ToList();
            if (emptyData.Any())
            {
                string msg = "";
                foreach (var item in emptyData)
                {
                    msg += "["+item.DiseaseName+"]"+"["+ item.DiseaseCoding+ "]:";
                }
                throw new Exception("当前未对码诊断:" + msg);
            }


            //主诊断
                var mainDiagnosisList = param.Where(c => c.IsMainDiagnosis == true)
                .Take(3).ToList();
            if (mainDiagnosisList.Any() == false) throw new Exception("主诊断不能为空!!!");
            resultData.DiagnosisDescribe = GetDiagnosisDescribe(resultData.DiagnosisDescribe, mainDiagnosisList);
            resultData.AdmissionMainDiagnosisIcd10 = CommonHelp.DiagnosisStr(mainDiagnosisList);
            //第二诊断
            var nextDiagnosisList = param.Where(c => c.IsMainDiagnosis == false)
                .ToList();
            if (mainDiagnosisList.Any())
            {
                var diagnosisIcd10Two = nextDiagnosisList.Take(3).ToList();
                resultData.DiagnosisDescribe = GetDiagnosisDescribe(resultData.DiagnosisDescribe, diagnosisIcd10Two);
                resultData.DiagnosisIcd10Two = CommonHelp.DiagnosisStr(diagnosisIcd10Two);
                if (nextDiagnosisList.Count > 3)
                {//第三诊断
                    resultData.DiagnosisIcd10Three = CommonHelp.DiagnosisStr(nextDiagnosisList.Where(d => !diagnosisIcd10Two.Contains(d)).Take(3).ToList());
                    resultData.DiagnosisDescribe = GetDiagnosisDescribe(resultData.DiagnosisDescribe, diagnosisIcd10Two);
                }
            }

            return resultData;
        }
        /// <summary>
        /// 获取诊断描述
        /// </summary>
        /// <param name="describe">描述参数</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetDiagnosisDescribe(string describe, List<InpatientDiagnosisDto> param)
        {
            string resultData = describe;

            foreach (var item in param)
            {
                if (!string.IsNullOrEmpty(resultData))
                {
                    if (resultData.Length < 100)//控制长度小于100
                    {
                        resultData = resultData + "," + item.DiseaseName;
                    }

                }
                else
                {
                    resultData = item.DiseaseName;
                }
            }

            return resultData;
        }
        /// <summary>
        /// 字符转码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string StrToTransCoding(byte[] param)
        {
            string resultData = null;
            if (param.Length > 0)
            {
                resultData = Encoding.ASCII.GetString(param, 1, 1023).Replace("\0", "");
            }
            Regex reg = new Regex("^[-+]?(([0-9]+)([.]([0-9]+))?|([.]([0-9]+))?)$");
            if (resultData != null)
            {
                if (reg.IsMatch(resultData) == false)
                {
                    resultData = Encoding.GetEncoding("GBK").GetString(param, 1, 1023).Replace("\0", "");
                    //获取字符是否包含??
                    bool flag = resultData.Contains("??");
                    if (flag) resultData = Encoding.GetEncoding("GBK").GetString(param, 1, 1023).Replace("\0", "");

                }

            }


            return resultData;
        }
        /// <summary>
        /// 获取结算反馈结果
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<PayMsgData> GetPayMsg(string param)
        {
            var paramNew = param.Replace("{", "").Replace("}", "").Replace(@"""", "");
            List<string> listMsg = new List<string>(paramNew.Split(','));
            var msg = new List<PayMsgData>();
            foreach (var item in listMsg)
            {
                var itemData = new List<string>(item.Split(':'));
                var itemName = itemData[0];
              
                msg.Add(new PayMsgData()
                {
                    Name = itemName.ToString(),
                    Value = itemData[1],
                });

            }

            return msg;
        }
        /// <summary>
        /// 获取查询日期
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static QueryDateBaseDto GetBillTime(string param)
        {
            var resultData = new QueryDateBaseDto();
            if (!string.IsNullOrWhiteSpace(param))
            {
                resultData.StartTime= param.Substring(0, 10)+ " 00:00:00.000";
                resultData.EndTime= param.Substring(param.Length - 10, 10)+ " 23:59:59.000";
            }
            return resultData;
        }

    }
    

}
