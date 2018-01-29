using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using MapsProject.Data.Repositories;
using MapsProject.Service.Interfaces;
using MapsProject.Service.Services;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace MapsProject.WEB
{
    /// <summary>
    /// Class for configure Unity DI-container.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Method for configure Unity DI-container.
        /// </summary>
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //UOW
            container.RegisterType<IUnitOfWork, EFUnitOfWork>(new InjectionConstructor("MapContext"));

            //Repositories
            //container.RegisterType<IRepository<MapObject>, MapObjectRepository>();

            //Service
            container.RegisterType<IMapObjectService, MapObjectService>();

            //Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}