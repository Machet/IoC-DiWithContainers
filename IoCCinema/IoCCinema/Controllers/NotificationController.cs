using IoCCinema.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IoCCinema.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationViewRepository _notificationRepository;

        public NotificationController(INotificationViewRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var data = _notificationRepository.GetNotificationsForUser(int.Parse(User.Identity.Name));
            return View(data);
        }
    }
}