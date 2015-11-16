using IoCCinema.Business.AuditLogging;
using IoCCinema.Business.Commands;
using IoCCinema.Controllers;
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

                Func<int, AuditLogger> loggerFactory =
                    (userId) => new AuditLogger(userId, new EFAuditRepository(perRequestStore.Context.Value));

                var handler = new ReserveSeatCommandHandler(perRequestStore.RoomRepository.Value, loggerFactory);
                var transactionalHandler = new TransactionalCommandHandler<ReserveSeatCommand>(perRequestStore.Context.Value, handler);
                var movieRepository = new EFHomeViewRepository(perRequestStore.Context.Value);
                return new HomeController(movieRepository, transactionalHandler);
            }

            return base.CreateController(requestContext, controllerName);
        }
    }
}