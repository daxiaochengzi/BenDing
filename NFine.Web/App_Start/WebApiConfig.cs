using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NFine.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
           config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            //var jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;
            ////获取或设置在序列化和反序列化期间如何处理空值。
            //jsonSettings.NullValueHandling = NullValueHandling.Ignore;//在序列化和反序列化对象时忽略空值。
            //// 移除XML序列化器
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            //config.Formatters.JsonFormatter.Indent = true;
            //// 解决json序列化时的循环引用问题
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            // 对 JSON 数据使用混合大小写。驼峰式,但是是javascript 首字母小写形式.
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss"
            });
        }
    }
}