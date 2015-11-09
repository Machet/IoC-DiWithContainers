using IoCCinema.Business;
using IoCCinema.Business.AuditLogging;
using IoCCinema.Business.Commands;
using IoCCinema.Business.Notifications;
using IoCCinema.Controllers;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Business;
using IoCCinema.DataAccess.Presentation;
using IoCCinema.DataAccess.Sms;
using IoCCinema.DataAccess.Smtp;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace IoCCinema
{
    public class PureControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            Type controllerType = GetControllerType(requestContext, controllerName);
            if (controllerType == typeof(HomeController))
            {
                var context = new CinemaContext();
                var roomRepository = new EfRoomRepository(context);
                var userRepository = new EfUserRepository(context);
                var templateRepository = new EfTemplateRepository();
                var notificationRepository = new EfNotificationRepository(context);

                var mailSender = new NetMailSender(notificationRepository);
                var smsSender = new MyMobileSmsSender(notificationRepository);

                var notifiers = new List<INotificationSender>
                {
                    new EmailNotificationSender(mailSender, templateRepository, userRepository),
                    new SmsNotificationSender(smsSender, templateRepository, userRepository),
                    new ApplicationNotificationSender(templateRepository, notificationRepository)
                };

                Func<int, AuditLogger> loggerFactory =
                    (userId) => new AuditLogger(userId, new EFAuditRepository(context));

                var handler = new ReserveSeatCommandHandler(roomRepository, notifiers, loggerFactory);
                var movieRepository = new EFHomeViewRepository(context);
                return new HomeController(movieRepository, handler);
            }

            return base.CreateController(requestContext, controllerName);
        }
    }
}