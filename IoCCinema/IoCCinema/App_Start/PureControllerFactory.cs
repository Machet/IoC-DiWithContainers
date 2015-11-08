using IoCCinema.Business;
using IoCCinema.Business.AuditLogging;
using IoCCinema.Business.Notifications;
using IoCCinema.Controllers;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.Repositories;
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
                var movieRepository = new EfMovieRepository(context);
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

                var movieService = new MovieService(movieRepository, roomRepository, notifiers, loggerFactory);

                return new HomeController(movieService);
            }

            return base.CreateController(requestContext, controllerName);
        }
    }
}