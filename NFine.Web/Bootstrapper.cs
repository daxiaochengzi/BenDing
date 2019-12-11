using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using BenDing.Repository.Interfaces.Web;
using BenDing.Repository.Providers.Web;
using BenDing.Service.Interfaces;
using BenDing.Service.Providers;
using NFine.Web.Model;
using StructureMap;


namespace NFine.Web
{
    public static class Bootstrapper
    {
        /// <summary>
        /// IOC ����������
        /// </summary>

        public static void Start()
        {
            var container = (IContainer) Ioc.Initialize();
            //  var container = ObjectFactory.Container;
            GlobalConfiguration.Configuration.DependencyResolver = new SmDependencyResolver(container);
        }
    }

    /// <summary>
    /// IOC��ʼ������
    /// </summary>
    public static class Ioc
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                //ͨ��StructureMap�Զ�ɨ�����з���������
                //x.Scan(scan =>
                //{
                //    scan.TheCallingAssembly();
                //    scan.WithDefaultConventions();
                //});
                #region Service
                x.For<IWebServiceBasicService>().Use<WebServiceBasicService>();
                x.For<IResidentMedicalInsuranceService>().Use<ResidentMedicalInsuranceService>();
                x.For<IUserService>().Use<UserService>();

                #endregion
                #region Repository
                x.For<IResidentMedicalInsuranceRepository>().Use<ResidentMedicalInsuranceRepository>();
                x.For<IMedicalInsuranceSqlRepository>().Use<MedicalInsuranceSqlRepository>();
                x.For<IWebBasicRepository>().Use<WebBasicRepository>();
                x.For<IHisSqlRepository>().Use<HisSqlRepository>();
                x.For<ISystemManageRepository>().Use<SystemManageRepository>();
                x.For<IOutpatientDepartmentRepository>().Use<OutpatientDepartmentRepository>();
                x.For<IOutpatientDepartmentService>().Use<OutpatientDepartmentService>();
                //
                #endregion

                //x.For<IUserService>().Use<UserService>(); //�������ֶ�������ȡ��һ
            });
            return ObjectFactory.Container;
        }
    }

    /// <summary>
    /// StructureMap ������
    /// </summary>
    public class StructureMapScope : IDependencyScope
    {
        protected IContainer Container;

        public StructureMapScope(IContainer container)
        {
            Container = container;
        }

        public void Dispose()
        {
            IDisposable disposable = (IDisposable) Container;
            if (disposable != null)
            {
                disposable.Dispose();
            }

            Container = null;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                return null;
            }

            try
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                    return Container.TryGetInstance(serviceType);

                return Container.GetInstance(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAllInstances<object>().Where(s => s.GetType() == serviceType);
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    public class SmDependencyResolver : StructureMapScope, IDependencyResolver
    {
        private IContainer _container;

        public SmDependencyResolver(IContainer container)
            : base(container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            _container = (IContainer) Ioc.Initialize();
            return new StructureMapScope(_container);
        }


        //    public static void RegisterTypes(IUnityContainer container)
        //    {
        //        #region Service
        //        container.RegisterType<IWebServiceBasicService, WebServiceBasicService>();
        //        container.RegisterType<IResidentMedicalInsuranceService, ResidentMedicalInsuranceService>();

        //        #endregion
        //        #region Repository
        //        container.RegisterType<IResidentMedicalInsuranceRepository, ResidentMedicalInsuranceRepository>();
        //        container.RegisterType<IMedicalInsuranceSqlRepository, MedicalInsuranceSqlRepository>();
        //        container.RegisterType<IWebBasicRepository, WebBasicRepository>();
        //        container.RegisterType<IHisSqlRepository, HisSqlRepository>();
        //        container.RegisterType<ISystemManageRepository, SystemManageRepository>();

        //        //
        //        #endregion

        //    }
        //}
    }
}