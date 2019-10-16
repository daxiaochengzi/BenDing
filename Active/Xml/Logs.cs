using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Xml
{
    public static class Logs
    {
        public static void LogWrite(LogParam param)
        {
            //string Is_msg = "";
            //string Is_day = DateTime.Now.Date.ToString("yyyy-MM-dd").Substring(0, 10);

            string path = System.IO.Directory.GetCurrentDirectory();
            string pathError = path + "\\logs";
            string pathErrorInfo = path + "\\logs\\internal-nlog.txt";
            if (!System.IO.Directory.Exists(pathError))
            {
                System.IO.Directory.CreateDirectory(pathError);
            }

            if (!System.IO.File.Exists(pathErrorInfo))
            {
                FileStream fs1 = new FileStream(pathErrorInfo, FileMode.Create, FileAccess.Write); //创建写入文件 
                fs1.Close();
            }

            if (!string.IsNullOrWhiteSpace(param.Msg))
            {
                 string Is_time_fish = null;
                 Is_time_fish = "【" + DateTime.Now.ToString() + "】" + "【Name】"+ param.Name+ 
                              "【OperatorCode】" + param.OperatorCode +"【msg】" +param.Msg;
                if (!string.IsNullOrWhiteSpace(param.Params))
                    Is_time_fish += "【Params】" + param.Params;
                if (!string.IsNullOrWhiteSpace(param.ResultData))
                    Is_time_fish += "【ResultData】" + param.ResultData;
                StreamWriter sw = File.AppendText(pathErrorInfo);
                // //获得字节数组
                string data = System.Text.Encoding.Default.GetBytes(Is_time_fish).ToString();
                // //开始写入
                sw.WriteLine(Is_time_fish.ToString());
                // //清空缓冲区、关闭流
                sw.Flush();
                sw.Close();
            }


        }

        public static string ToJson<T>(T t)
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(t);
            return result;
           
        }
    }
}
