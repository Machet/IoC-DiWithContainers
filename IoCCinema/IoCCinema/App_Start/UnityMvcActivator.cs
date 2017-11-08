using IoCCinema.CompositionRoot;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Linq;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IoCCinema.App_Start.UnityWebActivator), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(IoCCinema.App_Start.UnityWebActivator), "Shutdown")]

namespace IoCCinema.App_Start
{
    /// <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    public static class UnityWebActivator
    {
        private static UnityContainer _container;

        /// <summary>Integrates Unity when the application starts.</summary>
        public static void Start()
        {
            _container = DIConfig.Setup();

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(_container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));
            DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>Disposes the Unity container when the application is shut down.</summary>
        public static void Shutdown()
        {
            _container.Dispose();
        }
    }
}