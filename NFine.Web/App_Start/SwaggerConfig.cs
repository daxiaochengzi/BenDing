using System.Web.Http;
using WebActivatorEx;
using NFine.Web;
using Swashbuckle.Application;

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
                    })
                .EnableSwaggerUi(c =>
                    {
                       // c.InjectJavaScript(thisAssembly, "SwaggerDemo.Scripts.swaggerui.swagger_lang.js");//ÖÐÎÄ°ü
                    });
        }
    }
}
