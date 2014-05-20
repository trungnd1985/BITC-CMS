using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BITC.Library.Pattern;
using BITC.CMS.Data.Entity;
using BITC.Library.Data;

namespace BITC.CMS.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container
                .RegisterType<IDataContextAsync, BITCContext>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Page>, Repository<Page>>()
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerResolveLifetimeManager());
            //.RegisterType<IRepositoryAsync<Customer>, Repository<Customer>>()
            //.RegisterType<IRepositoryAsync<Product>, Repository<Product>>()
            //.RegisterType<IProductService, ProductService>()
            //.RegisterType<ICustomerService, CustomerService>()
            //.RegisterType<INorthwindStoredProcedures, NorthwindContext>(new PerRequestLifetimeManager())
            //.RegisterType<IStoredProcedureService, StoredProcedureService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}