using Autofac;
using Autofac.Integration.Mvc;
using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Business;
using IoCCinema.DataAccess.Presentation;
using IoCCinema.Presentation;
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

            builder.RegisterAssemblyTypes(dataAccessAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterType<LoginCommandHandler>().Named<ICommandHandler<LoginCommand>>("default");
            builder.RegisterDecorator<ICommandHandler<LoginCommand>>(
                (c, inner) => new TransactionalCommandHandler<LoginCommand>(inner, c.Resolve<CinemaContext>()),
                fromKey: "default");

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}