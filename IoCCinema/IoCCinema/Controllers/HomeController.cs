using System;
using System.Web.Mvc;
using IoCCinema.Presentation;
using IoCCinema.Business.Commands;

namespace IoCCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeViewRepository _repository;
        private readonly ICommandHandler<ReserveSeatCommand> _movieService;

        public HomeController(IHomeViewRepository repository, ICommandHandler<ReserveSeatCommand> service)
        {
            _repository = repository;
            _movieService = service;
        }

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

        [HttpPost]
        [Authorize]
        public ActionResult ChooseSeat(int seanseId, string seat)
        {
            var seatPosition = seat.Split('_');
            _movieService.Handle(new ReserveSeatCommand
            {
                UserId = 1,
                SeanseId = seanseId,
                SeatNumber = int.Parse(seatPosition[1]),
                SeatRow = int.Parse(seatPosition[0])
            });

            return RedirectToAction("ChooseSeat", new { seanseId });
        }

        [HttpGet]
        public ActionResult SeatTaken()
        {
            return View();
        }
    }
}