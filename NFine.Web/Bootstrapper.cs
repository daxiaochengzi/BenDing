using System.Web.Mvc;
using BenDingWeb.Service.Interfaces;
using BenDingWeb.Service.Providers;
using BenDingWeb.WebServiceBasic;
using Microsoft.Practices.Unity;
using NFine.Web.Model;
using Unity.Mvc4;

namespace NFine.Web
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();  
       
            RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        string connection = "123123";
        container.RegisterType<IGrammarNewService, GrammarNewService>();
        container.RegisterType<IWebServiceBasic, WebServiceBasic>();
        container.RegisterType<IDataBaseHelpService, DataBaseHelpService>();
        container.RegisterType<IDataBaseSqlServerService, DataBaseSqlServerService>();
        container.RegisterType<IWebServiceBasicService, WebServiceBasicService>();
        }
  }
}