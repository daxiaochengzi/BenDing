using System.Web.Http;
using WebActivatorEx;
using NFine.Web;
using Swashbuckle.Application;
using NFine.Web.App_Start;
using System;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Linq;
using System.Collections.Generic;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace NFine.Web
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "NFine.Web");
                    c.IncludeXmlComments(string.Format("{0}/bin/NFine.Web.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                    c.IncludeXmlComments(string.Format("{0}/bin/BenDing.Domain.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                  
                   // c.OperationFilter<HttpAuthHeaderFilter>();
                    //解决同样的接口名 传递不同参数
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                    c.CustomProvider((defaultProvider) => new SwaggerCacheProvider(defaultProvider,string.Format("{0}/bin/NFine.Web.xml", System.AppDomain.CurrentDomain.BaseDirectory)));
                    c.CustomProvider((defaultProvider) => new SwaggerCacheProvider(defaultProvider, string.Format("{0}/bin/BenDing.Domain.xml", System.AppDomain.CurrentDomain.BaseDirectory)));

                    c.DocumentFilter<HiddenApiFilter>(); //隐藏Swagger 自带API及隐藏具体Api

                })
                .EnableSwaggerUi(c =>
                {
                    //c.InjectJavaScript(System.Reflection.Assembly.GetExecutingAssembly(), "NFine.Web.script.swagger.js");
                });
        }
    }

    /// <summary>
    /// 隐藏Swagger默认接口
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public partial class HiddenApiAttribute : Attribute
    {
    }

    /// <summary>
    /// 隐藏Swagger默认接口
    /// </summary>
    public class HiddenApiFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            foreach (ApiDescription apiDescription in apiExplorer.ApiDescriptions)
            {
                var _key = "/" + apiDescription.RelativePath.TrimEnd('/');
                // 过滤 swagger 自带的接口
                if (_key.Contains("/api/Swagger") && swaggerDoc.paths.ContainsKey(_key))
                    swaggerDoc.paths.Remove(_key);

                //隐藏具体Api接口 需要在想隐藏的api 上面添加特性[HiddenApi]
                if (Enumerable
                    .OfType<HiddenApiAttribute>(apiDescription.GetControllerAndActionAttributes<HiddenApiAttribute>())
                    .Any())
                {
                    string key = "/" + apiDescription.RelativePath;
                    if (key.Contains("?"))
                    {
                        int idx = key.IndexOf("?", System.StringComparison.Ordinal);
                        key = key.Substring(0, idx);
                    }

                    swaggerDoc.paths.Remove(key);
                }
            }
        }
    }


}
