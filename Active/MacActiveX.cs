using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using BenDing.Service;
using BenDingActive.Model;
using BenDingActive.Service;
using BenDingActive.Xml;
using Newtonsoft.Json;

namespace BenDingActive
{
   // [Guid("65D8E97F-D3E2-462A-B389-241D7C38C518")]
    //public class MacActiveX : ActiveXControl
    public class MacActiveX 
    {  
        /// <summary>
        /// 居民
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>

        public string ResidentMedicalInsuranceMethod(string methodName, string baseParam)
        {
            string resultData = null;
           
            string strClass = "BenDingActive.Service.ResidentMedicalInsuranceService";           // 命名空间+类名
            resultData= MethodExecute(strClass, methodName, baseParam);
            return resultData;
        }
        /// <summary>
        /// 职工
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string WorkersMedicalInsuranceMethod(string methodName, string baseParam)
        {
            string resultData = null;
            var data = WorkersMedicalInsurance.ConnectAppServer_cxjb("cpq2677", "888888");
            string strClass = "BenDingActive.Service.WorkersMedicalInsuranceService";           // 命名空间+类名
            resultData = MethodExecute(strClass, methodName, baseParam);
            return resultData;
        }
        /// <summary>
        /// 单病种
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string SingleResidentMedicalInsuranceMethod(string methodName, string baseParam)
        {
            string resultData = null;
            var data = WorkersMedicalInsurance.ConnectAppServer_cxjb("cpq2677", "888888");
            string strClass = "BenDingActive.Service.SingleResidentMedicalInsuranceService";           // 命名空间+类名
            resultData = MethodExecute(strClass, methodName, baseParam);
            return resultData;
        }
        /// <summary>
        /// 异地
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        public string DifferentPlacesMedicalInsuranceMethod(string methodName, string baseParam)
        {
            string resultData = null;
            var data = WorkersMedicalInsurance.ConnectAppServer_cxjb("cpq2677", "888888");
            string strClass = "BenDingActive.Service.SingleResidentMedicalInsuranceService";           // 命名空间+类名
            resultData = MethodExecute(strClass, methodName, baseParam);
            return resultData;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="methodName"></param>
        /// <param name="baseParam"></param>
        /// <returns></returns>
        private string MethodExecute(string sign, string methodName, string baseParam)
        {
            string resultData = null;
            string strClass = sign;           // 命名空间+类名
            string strMethod = methodName;        // 方法名

            Type type;                          // 存储类
            Object obj;                         // 存储类的实例

            type = Type.GetType(strClass);      // 通过类名获取同名类
            obj = Activator.CreateInstance(type);       // 创建实例

            MethodInfo method;      // 获取方法信息
            object[] parameters = null; // 调用方法，参数为空

            // 注意获取重载方法，需要指定参数类型
            method = type.GetMethod(strMethod, new[] { typeof(string) });      // 获取方法信息
            parameters = new object[] { baseParam };
            if (method != null)
            {
                resultData = (string)method.Invoke(obj, parameters); // 调用方法，有参数
            }
            else
            {
                var apiJson = new ApiJsonResultDatas
                {
                    Code = false,
                    Message = "当前方法不存在!!!"
                };
                resultData=JsonConvert.SerializeObject(apiJson);
            }

            return resultData;
        }
    }
}
