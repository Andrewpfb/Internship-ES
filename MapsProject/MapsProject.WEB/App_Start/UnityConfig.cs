using System.Web.Http;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Repositories;
using MapsProject.Service.Interfaces;
using MapsProject.Service.Services;
using Unity;
using Unity.WebApi;
using Unity.Injection;

namespace MapsProject.WEB
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, EFUnitOfWork>(new InjectionConstructor("MapContext"));
            container.RegisterType<IMapObjectService, MapObjectService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}