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
using System.Collections.Concurrent;
using System.Xml;
using System.IO;

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
                    c.IncludeXmlComments(string.Format("{0}/bin/NFine.Web.xml",
                        System.AppDomain.CurrentDomain.BaseDirectory));
                    c.IncludeXmlComments(string.Format("{0}/bin/BenDing.Domain.xml",
                        System.AppDomain.CurrentDomain.BaseDirectory));
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));
                    //// c.OperationFilter<HttpAuthHeaderFilter>();
                    // //解决同样的接口名 传递不同参数

                    // c.CustomProvider((defaultProvider) => new SwaggerCacheProvider(defaultProvider, string.Format("{0}/bin/BenDing.Domain.xml", System.AppDomain.CurrentDomain.BaseDirectory)));

                    // c.DocumentFilter<HiddenApiFilter>(); //隐藏Swagger 自带API及隐藏具体Api

                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(thisAssembly, "NFine.Web.script.swagger_lang.js");
                });
        }

        public class CachingSwaggerProvider : ISwaggerProvider
        {
            private static ConcurrentDictionary<string, SwaggerDocument> _cache =
                new ConcurrentDictionary<string, SwaggerDocument>();

            private readonly ISwaggerProvider _swaggerProvider;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="swaggerProvider"></param>
            public CachingSwaggerProvider(ISwaggerProvider swaggerProvider)
            {
                _swaggerProvider = swaggerProvider;
            }/// <summary>
             /// 
             /// </summary>
             /// <param name="rootUrl"></param>
             /// <param name="apiVersion"></param>
             /// <returns></returns>

            public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
            {
                var cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
                SwaggerDocument srcDoc = null;
                //只读取一次
                if (!_cache.TryGetValue(cacheKey, out srcDoc))
                {
                    srcDoc = _swaggerProvider.GetSwagger(rootUrl, apiVersion);

                    srcDoc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", GetControllerDesc() } };
                    _cache.TryAdd(cacheKey, srcDoc);
                }
                return srcDoc;
            }

            /// <summary>
            /// 从API文档中读取控制器描述
            /// </summary>
            /// <returns>所有控制器描述</returns>
            public static ConcurrentDictionary<string, string> GetControllerDesc()
            {
                string xmlpath = string.Format("{0}/bin/NFine.Web.xml", System.AppDomain.CurrentDomain.BaseDirectory);
                ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
                if (File.Exists(xmlpath))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(xmlpath);
                    string type = string.Empty, path = string.Empty, controllerName = string.Empty;

                    string[] arrPath;
                    int length = -1, cCount = "Controller".Length;
                    XmlNode summaryNode = null;
                    foreach (XmlNode node in xmldoc.SelectNodes("//member"))
                    {
                        type = node.Attributes["name"].Value;
                        if (type.StartsWith("T:"))
                        {
                            //控制器
                            arrPath = type.Split('.');
                            length = arrPath.Length;
                            controllerName = arrPath[length - 1];
                            if (controllerName.EndsWith("Controller"))
                            {
                                //获取控制器注释
                                summaryNode = node.SelectSingleNode("summary");
                                string key = controllerName.Remove(controllerName.Length - cCount, cCount);
                                if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
                                {
                                    controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                                }
                            }
                        }
                    }
                }
                return controllerDescDict;
            }
        }
    }
    

    


}
