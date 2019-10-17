using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BenDingActive.Model.Params;
using Newtonsoft.Json;

namespace BenDingActive.Xml
{
    public class HttpHelp
    {
      
        public static T HttpPost<T>(string param,string methodName, T t) 
        {
            string result = null;

            var resultEntiy = t;
            string serviceAddress = "http://127.0.0.1:50839/Test/" + methodName + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";

            byte[] data = Encoding.UTF8.GetBytes(param);
            try
            {
                //写入请求流
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (request.GetResponse() is HttpWebResponse webresponse)
                    using (Stream s = webresponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(s, Encoding.UTF8);
                        result = reader.ReadToEnd();
                    }

                if (result != null)
                {
                    var resultData = JsonConvert.DeserializeObject<ApiJsonResultDatas>(result);
                    if (resultData.Code == false)
                    {
                        throw new Exception(resultData.Message);
                    }
                    else
                    {
                        resultEntiy = JsonConvert.DeserializeObject<T>(typeof(T).FullName =="ApiJsonResultData" ? result : resultData.Data.ToString());
                    }
                }

                    
                   
                

            }
            catch (Exception e)
            {
              throw new  Exception(e.Message);
            }

            return resultEntiy;

        }
        public static string HttpGet(string url)
        {
            string result = null;
            if (WebRequest.Create(url) is HttpWebRequest request)
            {
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded"; //链接类型
                if (request.GetResponse() is HttpWebResponse webresponse)
                    using (Stream s = webresponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(s, Encoding.UTF8);
                        result = reader.ReadToEnd();
                    }
            }

            return result;
        }


    }
}
