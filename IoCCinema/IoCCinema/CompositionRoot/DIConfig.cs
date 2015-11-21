using IoCCinema.Business;
using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.Business.DomainEvents;
using IoCCinema.Business.Lotery;
using IoCCinema.Business.Notifications;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.AuditLogging;
using IoCCinema.DataAccess.Business;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Collections.Generic;
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
            var businessAssembly = typeof(ICommandHandler<>).Assembly;

            container.Register<CinemaContext>(perRequest);
            container.Register<ICurrentUserProvider, ContextUserProvider>(Lifestyle.Singleton);
            container.Register<IWinChanceCalculatorFactory, SimpleInjectorWinChanceCalculatorFactory>(Lifestyle.Singleton);

            foreach (var repositorType in dataAccessAssembly.GetExportedTypes()
                .Where(t => t.Name.Contains("Repository")))
            {
                container.Register(repositorType.GetInterfaces().Single(), repositorType, perRequest);
            }

            container.RegisterDecorator(typeof(ICommandHandler<LoginCommand>), typeof(AuditingLoginCommandHandler));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(AuditingCommandHandler<>),
                p => !p.AppliedDecorators.Any(t => t.Name.Contains("Auditing")));
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionalCommandHandler<>));
            container.Register(typeof(ICommandHandler<>), new[] { businessAssembly });

            container.RegisterCollection(typeof(INotificationSender), new[] { businessAssembly });
            container.AppendToCollection(typeof(IDomainEventHandler<>), typeof(AuditOccurrenceEventHandler<>));
            container.RegisterCollection(typeof(IDomainEventHandler<>), new[] { businessAssembly, dataAccessAssembly });
            container.RegisterDecorator(typeof(IDomainEventHandler<>), typeof(AuditingEventHandler<>),
                p => !p.ImplementationType.Name.Contains("Audit"));


            container.Register<List<INotificationSender>>(() => container.GetAllInstances<INotificationSender>().ToList());
            DomainEventBus.Current = new SimpleInjectorEventBus(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}