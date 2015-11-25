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
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CinemaContext>().InstancePerRequest();
            builder.RegisterType<StringHasher>().SingleInstance();
            builder.RegisterType<ContextUserProvider>().As<ICurrentUserProvider>().SingleInstance();

            builder.RegisterType<EfMovieViewRepository>().As<IMovieViewRepository>().InstancePerRequest();
            builder.RegisterType<EfLoginRepository>().As<ILoginViewRepository>().InstancePerRequest();
            builder.RegisterType<EfAuthenticationRepository>().As<IAuthenticationRepository>().InstancePerRequest();

            builder.RegisterType<LoginCommandHandler>().Named<ICommandHandler<LoginCommand>>("default");
            builder.RegisterDecorator<ICommandHandler<LoginCommand>>(
                (c, inner) => new TransactionalCommandHandler<LoginCommand>(inner, c.Resolve<CinemaContext>()),
                fromKey: "default");

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}