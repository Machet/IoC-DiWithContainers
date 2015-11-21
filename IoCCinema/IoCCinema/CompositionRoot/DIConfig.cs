using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.Business.Lotery;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Business;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Linq;
using System.Web.Mvc;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        public static void Setup()
        {
            var container = new Container();
            var perRequest = new WebRequestLifestyle();
            var dataAccessAssembly = typeof(CinemaContext).Assembly;

            container.Register<CinemaContext>(perRequest);
            container.Register<ICurrentUserProvider, ContextUserProvider>(Lifestyle.Singleton);
            container.Register<IWinChanceCalculatorFactory, SimpleInjectorWinChanceCalculatorFactory>(Lifestyle.Singleton);

            foreach (var repositorType in dataAccessAssembly.GetExportedTypes()
                .Where(t => t.Name.Contains("Repository")))
            {
                container.Register(repositorType.GetInterfaces().Single(), repositorType, perRequest);
            }

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionalCommandHandler<>));
            container.Register(typeof(ICommandHandler<>), new[] { typeof(ICommandHandler<>).Assembly });

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}