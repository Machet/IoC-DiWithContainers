using System;
using System.Web.Mvc;
using IoCCinema.Presentation;

namespace IoCCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieViewRepository _repository;

        public HomeController(IMovieViewRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_repository.GetMovies(DateTime.Now));
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChooseSeat(int seanseId)
        {
            return View(_repository.GetRoomBySeanse(seanseId));
        }
    }
}