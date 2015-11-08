using System;
using System.Web.Mvc;

using IoCCinema.Business;

namespace IoCCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieService _movieService;

        public HomeController(MovieService service)
        {
            _movieService = service;
        }

        public ActionResult Index()
        {
            return View(_movieService.GetMovies(DateTime.Now));
        }

        [HttpGet]
        public ActionResult ChooseSeat(int movieRoomRelationId)
        {
            return View(_movieService.GetRoomByRelation(movieRoomRelationId));
        }

        [HttpPost]
        public ActionResult ChooseSeat(int movieRoomRelationId, string seat)
        {
            var seatPosition = seat.Split('_');
            if (_movieService.ReserveSeat(1, movieRoomRelationId, int.Parse(seatPosition[0]), int.Parse(seatPosition[1])))
            {
                return RedirectToAction("SeatTaken");
            }

            return RedirectToAction("ChooseSeat", new { movieRoomRelationId });
        }

        [HttpGet]
        public ActionResult SeatTaken()
        {
            return View();
        }
    }
}