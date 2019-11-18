using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using NFine.Code;


namespace NFine.Web
{
    public class ApiJsonResultData
    {
        
        #region .ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResultData" /> class.
        /// </summary>
        public ApiJsonResultData()
        {
            Messages = new string[0];
           
            Success = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResultData" /> class.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        public ApiJsonResultData(ModelStateDictionary modelState) : this()
        {
            this.AddModelState(modelState);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ApiJsonResultData" /> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("success")]
        public bool Success { get; set; } = true;

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        [JsonIgnore]
        public string[] Messages { get; set; }
        [JsonProperty("message")]
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
        /// Gets or sets the model.
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("redirect")]
        public string RedirectUrl { get; set; }
       


        [JsonProperty("reloadpage")]
        public bool ReloadPage { get; set; }

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
            

            try
            {
                if (jsonResultEntry.Success)
                    runMethod(jsonResultEntry);
            }

            catch (Exception e)
            {
                jsonResultEntry.Code = 1010;
              
            
              
            
               // jsonResultEntry.AddErrorMessage("系统错误:" + e.ToString());
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
            
            try
            {
                if (jsonResultEntry.Success)
                    await runMethod(jsonResultEntry);
            }


            catch (Exception e)
            {
                jsonResultEntry.Code = 1010;
                

                //jsonResultEntry.AddErrorMessage("系统错误:" + e.ToString());
                jsonResultEntry.AddErrorMessage("系统错误:" + (e.InnerException == null ? e.Message :
                                                    e.InnerException.InnerException == null ? e.InnerException.Message :
                                                    e.InnerException.InnerException.Message));


            }
            

            if (jsonResultEntry.Success) jsonResultEntry.Code = 0;
            return jsonResultEntry;
        }
    }


   

}
