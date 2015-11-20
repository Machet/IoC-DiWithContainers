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

        public ActionResult Index()
        {
            return View(_repository.GetMovies(DateTime.Now));
        }
    }
}