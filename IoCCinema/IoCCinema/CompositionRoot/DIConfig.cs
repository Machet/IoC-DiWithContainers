using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
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

            foreach (var repositorType in dataAccessAssembly.GetExportedTypes()
                .Where(t => t.Name.Contains("Repository")))
            {
                container.Register(repositorType.GetInterfaces().Single(), repositorType, perRequest);
            }

            container.RegisterDecorator<ICommandHandler<LoginCommand>, TransactionalCommandHandler<LoginCommand>>();
            container.Register<ICommandHandler<LoginCommand>, LoginCommandHandler>();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}