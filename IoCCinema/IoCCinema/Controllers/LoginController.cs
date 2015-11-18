using IoCCinema.Business.Commands;
using IoCCinema.Presentation;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace IoCCinema.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ICommandHandler<LoginCommand> _loginHandler;

        public LoginController(ILoginRepository loginRepository, ICommandHandler<LoginCommand> loginHandler)
        {
            _loginHandler = loginHandler;
            _loginRepository = loginRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(string userName, string password, string returnUrl)
        {
            Guid attemptId = Guid.NewGuid();

            _loginHandler.Handle(new LoginCommand
            {
                AttemptId = attemptId,
                Username = userName,
                Password = password
            });

            LoginAttemptDTO result = _loginRepository.GetLoginAttemptById(attemptId);
            if (result.Succeeded)
            {
                FormsAuthentication.SetAuthCookie(result.UserName, false);
                return !string.IsNullOrEmpty(returnUrl)
                    ? (ActionResult)Redirect(returnUrl)
                    : RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("LoginFailed", result.Message);
            return View();
        }
    }
}