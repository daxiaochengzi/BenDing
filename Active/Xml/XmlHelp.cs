using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using BenDingActive.Model;
using BenDingActive.Model.Dto;
using BenDingActive.Model.Params.Workers;
using BenDingActive.Service;
using Newtonsoft.Json;

namespace BenDingActive.Xml
{
  public static class XmlHelp
    {/// <summary>
    /// 转换为xml
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    /// <returns></returns>
        public static string ToXml<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            tStr = "";
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                dynamic value = item.GetValue(t, null);
                string str = null;
                if (value == null)
                {
                    str += "<" + name + ">" + "</" + name + ">";
                }
                else
                {
                    var datatype = value.GetType();
                    if (datatype.Name == "String")
                    {
                        str += "<" + name + ">" + value.ToString() + "</" + name + ">";
                    }
                    else
                    {
                        str += "<" + name + ">";
                        var rowstr = XmlRows(value);
                        if (!string.IsNullOrWhiteSpace(rowstr))
                        str += rowstr;
                        str += "</" + name + ">";

                    }
                }
                tStr += str.ToUpper();
            }
            return tStr;
          
        }
        public static string ToUnXml<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }
            tStr = "";
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                dynamic value = item.GetValue(t, null);
                string str = null;
                if (value == null)
                {
                    str += "<" + name + ">" + "</" + name + ">";
                }
                else
                {
                    var datatype = value.GetType();
                    if (datatype.Name == "String")
                    {
                        str += "<" + name + ">" + value.ToString() + "</" + name + ">";
                    }
                    else
                    {
                        str += "<" + name + ">";
                        var rowstr = XmlUnRows(value);
                        if (!string.IsNullOrWhiteSpace(rowstr))
                            str += rowstr;
                        str += "</" + name + ">";

                    }
                }
                tStr += str.ToUpper();
            }
            return tStr;

        }
        //public static bool SaveXml(string param)
        //{
        //    var strXml = param;
        //    bool result = false;
        //    if (string.IsNullOrWhiteSpace(strXml))
        //    {
        //        return result;
        //    }
        //    else
        //    {
        //        //创建XmlDocument对象
        //        XmlDocument xmlDoc = new XmlDocument();
        //        //XML的声明<?xml version="1.0" encoding="gb2312"?> 
        //        XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
        //        //追加xmldecl位置
        //        xmlDoc.AppendChild(xmlSM);
        //        //添加一个名为Gen的根节点
        //        XmlElement xml = xmlDoc.CreateElement("", "ROW", "");
        //        xml.InnerXml = strXml;
        //        xmlDoc.AppendChild(xml);

        //        string pathXml =null;
        //        if (GetOSBit.Is64Bit())
        //        {
        //            pathXml = "C:\\Program Files (x86)\\Microsoft\\BenDingActiveSetup" + "\\RequestParams.xml";
        //        }
        //        else
        //        {

        //        }


        //        if (File.Exists(pathXml))
        //        {
        //            File.Delete(pathXml);
        //            xmlDoc.Save(pathXml);
        //            result = true;
        //        }
        //        else
        //        {
        //            xmlDoc.Save(pathXml);
        //            result = true;
        //        }
        //        return result;
        //    }
        //}


        public static bool SaveXml<T>(T t)
        {
            var strXml = ToXml(t);
            bool result = false;
            if (string.IsNullOrWhiteSpace(strXml))
            {
                return result;
            }
            else
            {
                //创建XmlDocument对象
                XmlDocument xmlDoc = new XmlDocument();
                //XML的声明<?xml version="1.0" encoding="gb2312"?> 
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
                //追加xmldecl位置
                xmlDoc.AppendChild(xmlSM);
                //添加一个名为Gen的根节点
                XmlElement xml = xmlDoc.CreateElement("", "ROW", "");
                xml.InnerXml = strXml;
                xmlDoc.AppendChild(xml);

                string pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "RequestParams.xml";
                if (File.Exists(pathXml))
                {
                    File.Delete(pathXml);
                    xmlDoc.Save(pathXml);
                    result = true;
                }
                else
                {
                    xmlDoc.Save(pathXml);
                    result = true;
                }
                return result;
            }
        }

        public static bool SavePrescriptionUploadWorkersParam(PrescriptionUploadWorkersDetailListParam t,string num)
        {
            var strXml = ToUnXml(t);
            bool result = false;
            if (string.IsNullOrWhiteSpace(strXml))
            {
                return result;
            }
            else
            {
                //创建XmlDocument对象
                XmlDocument xmlDoc = new XmlDocument();
                //XML的声明<?xml version="1.0" encoding="gb2312"?> 
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
                //追加xmldecl位置
                xmlDoc.AppendChild(xmlSM);
                //添加一个名为Gen的根节点
                XmlElement xml = xmlDoc.CreateElement("", "ROWDATA", "");
                xml.InnerXml = strXml;
                xmlDoc.AppendChild(xml);

                string pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "cfcs\\cfmx"+ num + ".xml";
                if (File.Exists(pathXml))
                {
                    File.Delete(pathXml);
                    xmlDoc.Save(pathXml);
                    result = true;
                }
                else
                {
                    xmlDoc.Save(pathXml);
                    result = true;
                }

                return result;
            }
        }

        public static T DeSerializer<T>(string strXml) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                 throw new  Exception(ex.Message);
               // return null;
            }
        } 
        // <summary>
        // 实体转换为model
        // </summary>
        // <typeparam name = "T" ></ typeparam >
        // < param name="t"></param>
        // <returns></returns>
        public static T DeSerializerModel<T>(T t)
        {
            var result = t;
            string pathXml = null;
            if (GetOSBit.Is64Bit())
            {
                pathXml = "C:\\Program Files (x86)\\Microsoft\\BenDingActiveSetup" + "\\ResponseParams.xml";
            }
            else
            {
                pathXml = "C:\\Program Files\\Microsoft\\BenDingActiveSetup" + "\\ResponseParams.xml";
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            var resultData = JsonConvert.DeserializeObject<ResultData>(jsonText);
            if (resultData?.Row != null && resultData.Row.ToString() != "")
            {
                var jsonStr = JsonConvert.SerializeObject(resultData.Row);
                result = JsonConvert.DeserializeObject<T>(jsonStr);

            }

            return result;
        }



        // <summary>
        // 实体转换为model
        // </summary>
        // <typeparam name = "T" ></ typeparam >
        // < param name="t"></param>
        // <returns></returns>
        public static IniDto DeSerializerModels<T>(T t)
        {
            var result =new IniDto();
            string pathXml = null;
            if (GetOSBit.Is64Bit())
            {
                pathXml = "C:\\Program Files (x86)\\Microsoft\\BenDingActiveSetup" + "\\ResponseParams.xml";
            }
            else
            {
                pathXml = "C:\\Program Files\\Microsoft\\BenDingActiveSetup" + "\\ResponseParams.xml";
            }
            
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            var resultData = JsonConvert.DeserializeObject<ResultData>(jsonText);
            if (resultData?.Row != null && resultData.Row.ToString() != "")
            {
                var jsonStr = JsonConvert.SerializeObject(resultData.Row);
                result = JsonConvert.DeserializeObject<IniDto>(jsonStr);
              
              

            }

            return result;
        }

        public static string XmlSerialize<T>(T obj)
        {
            using (StringWriter sw = new StringWriter())
            {
                Type t = obj.GetType();
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(sw, obj);
                sw.Close();
                return sw.ToString();
            }
        }
        /// <summary>
        /// 获取行xml
        /// </summary>
        /// <param name="rowsObject"></param>
        /// <returns></returns>
        public  static string  XmlRows(dynamic rowsObject )
        {
            string str = null;
            var count = rowsObject.Count;
            //判断当前行是否存在
            if (Convert.ToInt32(count) > 0)
            {
                foreach (var entityItem in rowsObject)
                {
                    string rowsStr = "<Row>";
                    System.Reflection.PropertyInfo[] rows = entityItem.GetType()
                        .GetProperties(System.Reflection.BindingFlags.Instance |
                                       System.Reflection.BindingFlags.Public);

                    foreach (System.Reflection.PropertyInfo itemRows in rows)
                    {
                        string rowName = itemRows.Name;
                        dynamic rowValue = itemRows.GetValue(entityItem, null);

                        if (rowValue == null)
                        {
                            rowsStr += "<" + rowName + ">" + "</" + rowName + ">";
                        }
                        else
                        {
                            rowsStr += "<" + rowName + ">" + rowValue.ToString() + "</" + rowName + ">";
                        }

                      
                       
                    }
                    rowsStr += "</Row>";
                    str += rowsStr;
                }

               
            }
            return str;
        }
        public static string XmlUnRows(dynamic rowsObject)
        {
            string str = null;
            var count = rowsObject.Count;
            //判断当前行是否存在
            if (Convert.ToInt32(count) > 0)
            {
                foreach (var entityItem in rowsObject)
                {
                    string rowsStr = "<Row>";
                    System.Reflection.PropertyInfo[] rows = entityItem.GetType()
                        .GetProperties(System.Reflection.BindingFlags.Instance |
                                       System.Reflection.BindingFlags.Public);

                    foreach (System.Reflection.PropertyInfo itemRows in rows)
                    {
                        string rowName = itemRows.Name;
                        dynamic rowValue = itemRows.GetValue(entityItem, null);

                        if (rowValue == null)
                        {
                            rowsStr += "<" + rowName + ">" + "</" + rowName + ">";
                        }
                        else
                        {
                            rowsStr += "<" + rowName + ">" + rowValue.ToString() + "</" + rowName + ">";
                        }



                    }
                    rowsStr += "</Row>";
                    str += rowsStr;
                }


            }

            string news = str.Substring(5, str.Length - 5);
            news = news.Substring(0, news.Length - 6);
            return news;
        }

        /// <summary>
        /// 验证xml返回结果是否正确
        /// </summary>
        /// <param name="rowsName"></param>
        /// <returns></returns>
        public static ValidXmlDto ValidXml(string rowsName)
        {
            var result = new ValidXmlDto();

            string pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "RequestParams.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            XmlNode po_fhzNode = doc.SelectSingleNode("/ROW/PO_FHZ");
            result.PO_FHZ = po_fhzNode.InnerText;
            XmlNode po_msgNode = doc.SelectSingleNode("/ROW/PO_MSG");
            result.PO_MSG = po_msgNode.InnerText;
            if (result.PO_FHZ == "1")
            {
                var pO_RDXXNode = doc.SelectNodes("/ROW/"+ rowsName + "/ROW");
                result.IsRows = pO_RDXXNode.Count > 1 ? true : false;
            }
            return result;

        }
        /// <summary>
        /// 特殊疾病认定查询
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IdentificationSpecialQueryDto IdentificationSpecialQueryDeSerializerModel(IdentificationSpecialQueryDto t)
        {
            var result = new IdentificationSpecialQueryDto();
           
            string pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "RequestParams.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);

            XmlNode po_fhzNode = doc.SelectSingleNode("/ROW/PO_FHZ");
            result.PO_FHZ = po_fhzNode.InnerText;
            XmlNode po_msgNode = doc.SelectSingleNode("/ROW/PO_MSG");
            result.PO_MSG = po_msgNode.InnerText;
            if (result.PO_FHZ == "1")
            {
                 var po_RDXXRows= new List<PO_RDXX>(); 
                 var pO_RDXXNode = doc.SelectNodes("/ROW/PO_RDXX/ROW");
                foreach (XmlNode po_rdxxItem in pO_RDXXNode)
                {
                    var po_RDXXRow = DeSerializerModelXmlNode(new PO_RDXXRow(), po_rdxxItem);
                    var po_RDXXRowRow = po_RDXXRow.Row;
                    var po_JBXXNode= po_rdxxItem.SelectNodes("PO_JBXX/ROW");
                    //获取PO_JBXX节点数据
                    var po_JBXXRows = new List<PO_JBXX>();
                    foreach (XmlNode po_JBXXItem in po_JBXXNode)
                    {
                        var po_JBXXRow = DeSerializerModelXmlNode(new PO_JBXXRow(), po_JBXXItem);
                        po_JBXXRows.Add(po_JBXXRow.Row);
                    }

                    po_RDXXRowRow.PO_JBXXs = po_JBXXRows;
                    po_RDXXRows.Add(po_RDXXRowRow);
                }
                result.PO_RDXXs = po_RDXXRows;
            }
            return result;
        }
        public static T DeSerializerModelXmlNode<T>(T t,XmlNode xmlNode)
        {
               
                string jsonTextc = JsonConvert.SerializeXmlNode(xmlNode);
                var resultData = JsonConvert.DeserializeObject<T>(jsonTextc);
                return resultData;

        }
    }
}
              