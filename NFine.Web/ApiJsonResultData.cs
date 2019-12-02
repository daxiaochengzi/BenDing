using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

using BenDing.Domain.Xml;
using Newtonsoft.Json;
using NFine.Code;


namespace NFine.Web
{/// <summary>
/// 返回结果值封装
/// </summary>
    public class ApiJsonResultData
    {
        #region .ctor
        /// <summary>
        /// 
        /// </summary>
        public ApiJsonResultData(Object obj)
        {
            Messages = new string[0];
            Success = true;
            DataDescribe = CommonHelp.GetPropertyAliasDict(obj);
        }
        public ApiJsonResultData(ModelStateDictionary modelState, Object obj) : this(obj)
        {
            this.AddModelState(modelState);
        }
        #endregion

        #region Properties

        /// <summary>
        /// 是否成功
        /// </summary>

        public bool Success { get; set; } = true;

        /// <summary>
        /// messages.
        /// </summary>
        [JsonIgnore]
        public string[] Messages { get; set; }
        /// <summary>
        /// 消息
        /// </summary>

        public string Message
        {
            get
            {
                if (Messages != null && Messages.Any())
                    return string.Join(",", Messages);
                return string.Empty;
            }
        }

        /// <summary>
        /// 数据
        /// </summary>

        public object Data { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>

        public int Code { get; set; }
        /// <summary>
        /// 数据描述文档
        /// </summary>
        public Dictionary<string, string> DataDescribe { get; set; }
        #endregion
        #region methods
        public void AddMessage(string message)
        {
            Messages = Messages.Concat(new[] { message }).ToArray();

        }
        public void AddErrorMessage(string message)
        {
            Messages = Messages.Concat(new[] { message }).ToArray();

            Success = false;
        }
        public void AddException(Exception e)
        {

            AddErrorMessage(e.Message);
        }

        /// <summary>
        /// Adds the state of the model.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <returns></returns>
        public void AddModelState(ModelStateDictionary modelState)
        {
            foreach (var ms in modelState)
            {
                foreach (var err in ms.Value.Errors)
                {
                    this.AddFieldError(ms.Key.ToLower(), err.ErrorMessage);
                }
            }
        }
        /// <summary>
        /// Adds the field error.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public void AddFieldError(string fieldName, string message)
        {
            Success = false;

            AddErrorMessage(message);
        }
        #endregion
    }/// <summary>
     /// 
     /// </summary>
    public static class ApiJsonResultEntryExtensions
    {


        public static ApiJsonResultData RunWithTry(this ApiJsonResultData jsonResultEntry, Action<ApiJsonResultData> runMethod)
        {
            string Is_day = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ";

            var log = LogFactory.GetLogger("ini".GetType().ToString());

            try
            {
                if (jsonResultEntry.Success)
                    runMethod(jsonResultEntry);
            }

            catch (Exception e)
            {
                jsonResultEntry.Code = 1010;

                log.Error(Is_day + e.ToString());
                jsonResultEntry.AddErrorMessage("系统错误:" + (e.InnerException == null ? e.Message : e.InnerException.InnerException == null ? e.InnerException.Message : e.InnerException.InnerException.Message));

            }

            if (jsonResultEntry.Success) jsonResultEntry.Code = 0;
            return jsonResultEntry;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonResultEntry"></param>
        /// <param name="runMethod"></param>
        /// <returns></returns>
        public static async Task<ApiJsonResultData> RunWithTryAsync(this ApiJsonResultData jsonResultEntry, Func<ApiJsonResultData, Task> runMethod)
        {
            string Is_day = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] ";
            var log = LogFactory.GetLogger("ini".GetType().ToString());
            try
            {
                if (jsonResultEntry.Success)
                    await runMethod(jsonResultEntry);
            }
            catch (Exception e)
            {
                jsonResultEntry.Code = 1010;
                log.Error(Is_day + e.ToString());
                jsonResultEntry.AddErrorMessage("系统错误:" + (e.InnerException == null ? e.Message :
                                                    e.InnerException.InnerException == null ? e.InnerException.Message :
                                                    e.InnerException.InnerException.Message));
            }

            if (jsonResultEntry.Success) jsonResultEntry.Code = 0;
            return jsonResultEntry;
        }
    }




}
