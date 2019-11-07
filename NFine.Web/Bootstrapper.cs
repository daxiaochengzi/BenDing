using System.Web.Mvc;
using BenDing.Repository.Interfaces.Web;
using BenDing.Repository.Providers.Web;
using BenDing.Service.Interfaces;

using BenDing.Service.Providers;

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
            #region Service
            container.RegisterType<IWebServiceBasicService, WebServiceBasicService>();

            #endregion
            #region Repository
            container.RegisterType<IResidentMedicalInsuranceRepository, ResidentMedicalInsuranceRepository>();
            container.RegisterType<IBaseSqlServerRepository, BaseSqlServerRepository>();
            container.RegisterType<IWebBasicRepository, WebBasicRepository>();
            container.RegisterType<IDataBaseHelpRepository, DataBaseHelpRepository>();
            container.RegisterType<ISystemManageRepository, SystemManageRepository>();
            
            //
            #endregion

        }
    }
}