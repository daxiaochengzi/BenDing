using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenDingActive.Xml
{
  public  class KeyboardValue
    {
        private KeyPressEventHandler myKeyEventHandeler = null;//按键钩子
        BardCodeHooK _keyboardHook = new BardCodeHooK();
        IniFile _iniFile = new IniFile();
        private string _pwd = null;
       
        private void K_hook_KeyPressEvent(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar.ToString() != "\r")
            {
                _pwd += e.KeyChar.ToString();
            }

          

            if (e.KeyChar.ToString() == "\r")
            {


                if (_pwd.Length > 6)
                {
                    string _pwdNew = GetStr(_pwd);
                    _iniFile.IniWriteValue("BenDingActive", "pwd", _pwdNew);

                }
                else
                {
                    _iniFile.IniWriteValue("BenDingActive", "pwd", _pwd);
                }

                if (myKeyEventHandeler != null)
                {
                    _keyboardHook.OnKeyPressEvent -= myKeyEventHandeler; //取消按键事件
                    myKeyEventHandeler = null;
                    _keyboardHook.Stop(); //关闭键盘钩子

                }

            }


        }

        public string GetStr(string param)
        {
            string resultData = "";
            for (int i = 1; i < param.Length + 1; i = i + 2)
            {
                if (resultData.Length < 6)
                {
                    resultData += param.Substring(i, 1);
                }


            }
            return resultData;
        }

        public void Start()
        {
            _pwd = null;
          
            myKeyEventHandeler = new KeyPressEventHandler(K_hook_KeyPressEvent);
            _keyboardHook.OnKeyPressEvent += K_hook_KeyPressEvent;
            _keyboardHook.Start();
        }
    }
}

