using System.IO;
using System.Reflection;
using System.Web.Http;
using NFine.Code;
using System.Web.Mvc;
using System.Web.Routing;
using NFine.Web.App_Start;
using NFine.Web.Model;
using System.Linq;

namespace NFine.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 启动应用程序
        /// </summary>
        protected void Application_Start()
        {
           
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.Config();
            Bootstrapper.Initialise();


        }
        /// <summary>
        /// 跨Frame设置
        /// </summary>

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("X-Frame-Options");
            Response.AddHeader("X-Frame-Options", "AllowAll");
        }


    }


}
