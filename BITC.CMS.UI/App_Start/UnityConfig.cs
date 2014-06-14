using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BITC.Library.Pattern;
using BITC.CMS.Data.Entity;
using BITC.Library.Data;
using BITC.CMS.UI.Controllers;
using BITC.CMS.Service;
using BITC.CMS.UI.Areas.Admin.Controllers;

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
                .RegisterType<IRepositoryAsync<Media>, Repository<Media>>()
                .RegisterType<IRepositoryAsync<BlogTag>, Repository<BlogTag>>()
                .RegisterType<IRepositoryAsync<BlogEntry>, Repository<BlogEntry>>()
                .RegisterType<IRepositoryAsync<Client>, Repository<Client>>()
                .RegisterType<IRepositoryAsync<Module>, Repository<Module>>()
                .RegisterType<IRepositoryAsync<Project>,Repository<Project>>()
                .RegisterType<IRepositoryAsync<ProjectCategory>, Repository<ProjectCategory>>()
                .RegisterType<IBlogService, BlogService>()
                .RegisterType<IPageService, PageService>()
                .RegisterType<IBlogTagService, BlogTagService>()
                .RegisterType<IModuleService, ModuleService>()
                .RegisterType<ISettingService, SettingService>()
                .RegisterType<IMenuService, MenuService>()
                .RegisterType<IProjectCategoryService, ProjectCategoryService>()
                .RegisterType<IProjectService, ProjectService>()
                .RegisterType<AuthenticationController>(new InjectionConstructor())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerResolveLifetimeManager());
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}