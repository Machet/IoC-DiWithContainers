using System;
using System.Web.Mvc;
using IoCCinema.Presentation;
using IoCCinema.Business.Commands;

namespace IoCCinema.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IMovieViewRepository _repository;
        private readonly ICommandHandler<ReserveSeatCommand> _movieService;

        public ReservationController(IMovieViewRepository repository, ICommandHandler<ReserveSeatCommand> service)
        {
            _repository = repository;
            _movieService = service;
        }

        [HttpGet]
        public ActionResult ChooseSeat(int seanseId)
        {
            return View(_repository.GetRoomBySeanse(seanseId));
        }

        [HttpPost]
        public ActionResult ChooseSeat(int seanseId, string seat)
        {
            var seatPosition = seat.Split('_');
            _movieService.Handle(new ReserveSeatCommand
            {
                UserId = int.Parse(User.Identity.Name),
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