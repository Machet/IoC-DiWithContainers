using IoCCinema.Business.Authentication;
using IoCCinema.Business.Commands;
using IoCCinema.Controllers;
using IoCCinema.DataAccess.AuditLogging;
using IoCCinema.DataAccess.Business;
using IoCCinema.DataAccess.Presentation;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace IoCCinema.CompositionRoot
{
    public class PureControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            Type controllerType = GetControllerType(requestContext, controllerName);
            if (controllerType == typeof(HomeController))
            {
                var perRequestStore = PerRequestStore.Current;
                var handler = new ReserveSeatCommandHandler(perRequestStore.RoomRepository.Value);
                var auditingHandler = new AuditingCommandHandler<ReserveSeatCommand>(handler, perRequestStore.AuditLogger.Value);
                var transactionalHandler = new TransactionalCommandHandler<ReserveSeatCommand>(auditingHandler, perRequestStore.Context.Value);
                var movieRepository = new EFHomeViewRepository(perRequestStore.Context.Value);
                return new HomeController(movieRepository, transactionalHandler);
            }

            if (controllerType == typeof(LoginController))
            {
                var perRequestStore = PerRequestStore.Current;
                var handler = new LoginCommandHandler(new EfAuthenticationRepository(perRequestStore.Context.Value), new StringHasher());
                var auditingHandler = new AuditingCommandHandler<LoginCommand>(handler, perRequestStore.AuditLogger.Value);
                var transactionalHandler = new TransactionalCommandHandler<LoginCommand>(auditingHandler, perRequestStore.Context.Value);
                return new LoginController(new EfLoginRepository(perRequestStore.Context.Value), transactionalHandler);
            }

            return base.CreateController(requestContext, controllerName);
        }
    }
}