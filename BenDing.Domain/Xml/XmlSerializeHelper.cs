using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using BenDing.Domain.Models.Dto.Resident;
using Newtonsoft.Json;

namespace BenDing.Domain.Xml
{
  public static class XmlSerializeHelper
    { /// <summary>
        /// 将实体对象转换成XML
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体对象</param>
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
        /// <summary>
        /// 医保转xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string MedicalInsuranceXmlSerialize<T>(T o)
        {
            var encoding = Encoding.GetEncoding("GBK");
            if (o == null)
                throw new ArgumentNullException("实体不能为空!!!");

            var ser = new XmlSerializer(o.GetType());
            using (var ms = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(ms, encoding))
                {
                    writer.Formatting = System.Xml.Formatting.Indented;
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");//把命名空间设置为空，这样就没有命名空间了
                    ser.Serialize(writer, o, ns);
                }
                var encodingData = encoding.GetString(ms.ToArray());
                return System.Text.RegularExpressions.Regex.Replace(encodingData, "^[^<]", "");
            }
        }
        /// <summary>
        /// 基层转xml
        /// </summary>
        /// <param name="o"></param>
        /// <param name="encoding"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static string HisXmlSerialize<T>(T o)
        {
            var encoding = Encoding.UTF8;
            if (o == null)
                throw new ArgumentNullException("实体不能为空!!!");
           
            var ser = new XmlSerializer(o.GetType());
            using (var ms = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(ms, encoding))
                {
                    writer.Formatting = System.Xml.Formatting.Indented;
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");//把命名空间设置为空，这样就没有命名空间了
                    ser.Serialize(writer, o, ns);
                }
               var encodingData = encoding.GetString(ms.ToArray());
               return System.Text.RegularExpressions.Regex.Replace(encodingData, "^[^<]", "");
            }
        }

        /// <summary>
        /// 将XML转换成实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="strXML">XML</param>
        public static T DESerializer<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将XML转换成实体对象异常", ex);
            }
        }
        /// <summary>
        /// 回参
        /// </summary>
        /// <returns></returns>
        public static string XmlBackParam()
        {
            var result = "";
            string pathXml = null;
            var valid = new ValidXmlDto();
            pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "bin\\ResponseParams.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            XmlNode po_fhzNode = doc.SelectSingleNode("/ROW/PO_FHZ");
            valid.PO_FHZ = po_fhzNode.InnerText;
            XmlNode po_msgNode = doc.SelectSingleNode("/ROW/PO_MSG");
            valid.PO_MSG = po_msgNode.InnerText;
            if (valid.PO_FHZ == "1")
            {
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                var resultData = JsonConvert.DeserializeObject<ResultData>(jsonText);
                if (resultData?.Row != null && resultData.Row.ToString() != "")
                {
                 
                    result = doc.InnerXml;
                }
            }
            else
            {
                throw new SystemException(valid.PO_MSG);
            }
            doc = null;
            return result;
        }
        /// <summary>
        /// 入参
        /// </summary>
        /// <returns></returns>
        public static string XmlParticipationParam()
        {
            var result = "";
            string pathXml = null;
            var valid = new ValidXmlDto();
            pathXml = System.AppDomain.CurrentDomain.BaseDirectory + "bin\\RequestParams.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(pathXml);
            result = doc.InnerXml;
            doc = null;
            return result;
        }
    }
}
