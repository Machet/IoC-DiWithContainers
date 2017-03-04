using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using IoCCinema.Business;
using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.Business.DomainEvents;
using IoCCinema.Business.Lotery;
using IoCCinema.Business.Notifications;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.AuditLogging;
using IoCCinema.DataAccess.Business;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        public static void Setup()
        {
            var builder = new ContainerBuilder();
            var dataAccessAssembly = typeof(CinemaContext).Assembly;
            var businessAssembly = typeof(ICommand).Assembly;

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CinemaContext>().InstancePerRequest();
            builder.RegisterType<StringHasher>().SingleInstance();
            builder.RegisterType<AuditLogger>().InstancePerRequest();
            builder.RegisterType<ContextUserProvider>().As<ICurrentUserProvider>().SingleInstance();
            builder.RegisterType<AutofacWinChanceCalculatorFactory>()
                .As<IWinChanceCalculatorFactory>().InstancePerRequest();

            builder.RegisterAssemblyTypes(businessAssembly)
                .Where(t => t.IsAssignableTo<IWinChanceCalculator>())
                .Named<IWinChanceCalculator>(t => t.Name.Replace("UserWinChanceCalculator", string.Empty))
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(dataAccessAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

			builder.RegisterAssemblyTypes(businessAssembly)
				.AsClosedTypesOf(typeof(ICommandHandler<>), "default");

            builder.RegisterGenericDecorator(
                typeof(AuditingCommandHandler<>),
                typeof(ICommandHandler<>),
                fromKey: "default", toKey: "auditing");

            builder.RegisterGenericDecorator(
                typeof(TransactionalCommandHandler<>),
                typeof(ICommandHandler<>),
                fromKey: "auditing");

            builder.RegisterDecorator<ICommandHandler<LoginCommand>>(
                (c, inner) => new AuditingLoginCommandHandler(inner, c.Resolve<AuditLogger>()),
                fromKey: "default", toKey: "auditing");

            builder.RegisterAssemblyTypes(businessAssembly)
                .AssignableTo<INotificationSender>()
                .AsImplementedInterfaces();
            builder.Register(c => c.Resolve<IEnumerable<INotificationSender>>().ToList())
                .As<List<INotificationSender>>();

            builder.RegisterGeneric(typeof(AuditOccurrenceEventHandler<>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(businessAssembly)
				.AsClosedTypesOf(typeof(ICommandHandler<>), "default")
                .PreserveExistingDefaults()
                .InstancePerRequest();

            builder.RegisterGenericDecorator(
                typeof(AuditingEventHandler<>),
                typeof(IDomainEventHandler<>),
                fromKey: "defaultEvent");

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            DomainEventBus.Current = new AutofacDomainEventBus();
        }
    }
}