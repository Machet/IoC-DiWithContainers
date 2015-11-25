using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.Business.Lotery;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Business;
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
                .Where(t => t.IsClosedTypeOf(typeof(ICommandHandler<>)))
                .As(t => t.GetInterfaces()
                    .Where(i => i.IsClosedTypeOf(typeof(ICommandHandler<>)))
                    .Select(i => new KeyedService("default", i)));

            builder.RegisterGenericDecorator(
                typeof(TransactionalCommandHandler<>),
                typeof(ICommandHandler<>),
                fromKey: "default");

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}