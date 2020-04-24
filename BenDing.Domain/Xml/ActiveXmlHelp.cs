using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BenDing.Domain.Xml
{
  public  class ActiveXmlHelp
    {
        /// <summary>
        ///  回参
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="isAbnormal"></param>
        /// <returns></returns>
        public static T DeSerializerModel<T>(T t, bool isAbnormal)
        {
            var result = t;
            string pathXml = null;
            string copyPathXml = null;
            var valid = new ValidXmlDto();
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                pathXml = @"C:\Program Files (x86)\Microsoft\BenDingActiveSetup\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files (x86)\Microsoft\BenDingActiveSetup\";
            }
            else
            {
                pathXml = @"C:\Program Files\Microsoft\BenDingActiveSetup\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files\Microsoft\BenDingActiveSetup\";
            }
            File.Copy(pathXml, copyPathXml + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml");

            // pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "ResponseParams.xml";

            if (!System.IO.File.Exists(pathXml))
            {
                throw new SystemException("ResponseParams文件不存在!!!");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            var fhz = doc.SelectSingleNode("/ROW/PO_FHZ");
            if (fhz != null)
            {
                valid.PO_FHZ = fhz.InnerText;
            }
            else
            {
                var fhzNew = doc.SelectSingleNode("/row/po_fhz");
                valid.PO_FHZ = fhzNew.InnerText;
            }
            var msg = doc.SelectSingleNode("/ROW/PO_MSG");
            if (msg != null)
            {
                valid.PO_MSG = msg.InnerText;
            }
            else
            {
                var msgNew = doc.SelectSingleNode("/row/po_msg");
                valid.PO_MSG = msgNew.InnerText;
            }

            if (isAbnormal == true)
            {
                if (valid.PO_FHZ != "1")
                {
                    throw new SystemException(valid.PO_MSG);
                }
            }
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            var resultData = JsonConvert.DeserializeObject<Models.Dto.Resident.ResultData>(jsonText);
            if (resultData?.Row != null && resultData.Row.ToString() != "")
            {
                var jsonStr = JsonConvert.SerializeObject(resultData.Row);
                result = JsonConvert.DeserializeObject<T>(jsonStr);
            }

            doc = null;
            return result;
        }
        /// <summary>
        ///  返回装换为json
        /// </summary>
        /// <returns></returns>
        public static string SerializerModelJson()
        {
            string jsonStr = null;
            string pathXml = null;
            string copyPathXml = null;
            var valid = new ValidXmlDto();
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                pathXml = @"C:\Program Files (x86)\Microsoft\BenDingActiveSetup\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files (x86)\Microsoft\BenDingActiveSetup\";
            }
            else
            {
                pathXml = @"C:\Program Files\Microsoft\BenDingActiveSetup\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files\Microsoft\BenDingActiveSetup\";
            }
            File.Copy(pathXml, copyPathXml + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml");

            // pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "ResponseParams.xml";

            if (!System.IO.File.Exists(pathXml))
            {
                throw new SystemException("ResponseParams文件不存在!!!");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            var fhz = doc.SelectSingleNode("/ROW/PO_FHZ");
            if (fhz != null)
            {
                valid.PO_FHZ = fhz.InnerText;
            }
            else
            {
                var fhzNew = doc.SelectSingleNode("/row/po_fhz");
                valid.PO_FHZ = fhzNew.InnerText;
            }
            var msg = doc.SelectSingleNode("/ROW/PO_MSG");
            if (msg != null)
            {
                valid.PO_MSG = msg.InnerText;
            }
            else
            {
                var msgNew = doc.SelectSingleNode("/row/po_msg");
                valid.PO_MSG = msgNew.InnerText;
            }

            if (valid.PO_FHZ != "1")
            {
                throw new SystemException(valid.PO_MSG);
            }
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            var resultData = JsonConvert.DeserializeObject<Models.Dto.Resident.ResultData>(jsonText);
            if (resultData?.Row != null && resultData.Row.ToString() != "")
            {
                jsonStr = JsonConvert.SerializeObject(resultData.Row);

            }

            doc = null;
            return jsonStr;
        }
        //实体转换
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
                throw new Exception(ex.Message);
                // return null;
            }
        }
        public static string DeSerializerModelStr(string rowsName)
        {

            string resultData = null;
            var valid = new ValidXmlDto();
            string pathXml = null;
            string copyPathXml = null;
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                pathXml = @"C:\Program Files (x86)\Microsoft\BenDingActiveSetup\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files (x86)\Microsoft\BenDingActiveSetup\";
            }
            else
            {
                pathXml = @"C:\Program Files\Microsoft\BenDingActiveSetup\" + "ResponseParams.xml";
                copyPathXml = @"C:\Program Files\Microsoft\BenDingActiveSetup\";
            }
            File.Copy(pathXml, copyPathXml + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            if (!System.IO.File.Exists(pathXml))
            {
                throw new SystemException("ResponseParams文件不存在!!!");
            }
            var fhz = doc.SelectSingleNode("/ROW/PO_FHZ");
            if (fhz != null)
            {
                valid.PO_FHZ = fhz.InnerText;
            }
            else
            {
                var fhzNew = doc.SelectSingleNode("/row/po_fhz");
                valid.PO_FHZ = fhzNew.InnerText;
            }
            var msg = doc.SelectSingleNode("/ROW/PO_MSG");
            if (msg != null)
            {
                valid.PO_MSG = msg.InnerText;
            }
            else
            {
                var msgNew = doc.SelectSingleNode("/row/po_msg");
                valid.PO_MSG = msgNew.InnerText;
            }
            if (valid.PO_FHZ == "1")
            {
                var rowNode = doc.SelectSingleNode("/ROW/" + rowsName) ?? doc.SelectSingleNode("/row/" + rowsName);
                string strIni = "xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'";
                resultData = "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + "<" + rowsName + " " + strIni + ">" + rowNode.InnerXml + "</" + rowsName + ">";

            }
            else
            {
                throw new SystemException(valid.PO_MSG);
            }

            doc = null;
            return resultData;
        }
        /// <summary>
        /// 实体转换保存入参
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool SaveXmlEntity<T>(T t)
        {
            string pathXml = null;
            var strXmls = XmlSerialize(t);
            bool result = false;
            if (string.IsNullOrWhiteSpace(strXmls))
            {
                return result;
            }
            else
            {
                var is64Bit = Environment.Is64BitOperatingSystem;
                if (is64Bit)
                {
                    pathXml = @"C:\Program Files (x86)\Microsoft\BenDingActiveSetup\" + "RequestParams.xml";
                }
                else
                {
                    pathXml = @"C:\Program Files\Microsoft\BenDingActiveSetup\" + "RequestParams.xml";
                }
                //pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "RequestParams.xml";

                //创建XmlDocument对象
                XmlDocument xmlDocXml = new XmlDocument();
                xmlDocXml.LoadXml(strXmls);

                //创建XmlDocument对象
                XmlDocument xmlDoc = new XmlDocument();
                //XML的声明<?xml version="1.0" encoding="gb2312"?> 
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
                //追加xmldecl位置
                xmlDoc.AppendChild(xmlSM);
                //添加一个名为Row的根节点
                XmlElement xml = xmlDoc.CreateElement("", "ROW", "");
                string strXml = xmlDocXml.SelectSingleNode("ROW").InnerXml.ToString();
                xml.InnerXml = strXml;
                xmlDoc.AppendChild(xml);


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
                xmlDoc = null;
                return result;
            }
        }
        /// <summary>
        /// 字符串保存入参
        /// </summary>
        /// <param name="strXmls"></param>
        /// <returns></returns>
        public static bool SaveXmlStr(string strXmls)
        {
            string pathXml = null;

            bool result = false;
            if (string.IsNullOrWhiteSpace(strXmls))
            {
                return result;
            }
            else
            {
                var is64Bit = Environment.Is64BitOperatingSystem;
                if (is64Bit)
                {
                    pathXml = @"C:\Program Files (x86)\Microsoft\BenDingActiveSetup\" + "RequestParams.xml";
                }
                else
                {
                    pathXml = @"C:\Program Files\Microsoft\BenDingActiveSetup\" + "RequestParams.xml";
                }
                //pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "RequestParams.xml";

                //创建XmlDocument对象
                XmlDocument xmlDocXml = new XmlDocument();
                xmlDocXml.LoadXml(strXmls);

                //创建XmlDocument对象
                XmlDocument xmlDoc = new XmlDocument();
                //XML的声明<?xml version="1.0" encoding="gb2312"?> 
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
                //追加xmldecl位置
                xmlDoc.AppendChild(xmlSM);
                //添加一个名为Row的根节点
                XmlElement xml = xmlDoc.CreateElement("", "ROW", "");
                string strXml = xmlDocXml.SelectSingleNode("ROW").InnerXml.ToString();
                xml.InnerXml = strXml;
                xmlDoc.AppendChild(xml);


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
                xmlDoc = null;
                return result;
            }
        }

        public static string XmlSerialize<T>(T obj)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }
    }
}
