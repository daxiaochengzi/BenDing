using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BenDingActive.Xml
{
   public class IniFile
    {
        public string path=System.IO.Directory.GetCurrentDirectory() + "\\BenDingActive.ini";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);

        [DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal, int size, string filePath);

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }


        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);

            return temp.ToString();

        }

        ///// <summary>
        ///// 获取输入的密码
        ///// </summary>
        ///// <returns></returns>
        //public string GetPwd()
        //{
        //    string fileUserId = IniReadValue("BenDingActive", "Pwd");
        //    return fileUserId;
        //}

    }
}

