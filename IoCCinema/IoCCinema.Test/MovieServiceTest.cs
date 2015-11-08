using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IoCCinema.Business;
using Moq;
using System.Collections.Generic;
using IoCCinema.Business.AuditLogging;
using IoCCinema.Business.DTO;
using IoCCinema.Business.Notifications;

namespace IoCCinema.Test
{
    [TestClass]
    public class MovieServiceTest
    {
        [TestMethod]
        public void MovieService_ShouldAllowToReserveSeat_WhenReservationIsMadeBeforeMovieStart()
        {
            // given
            var reservationTime = TimeSpan.FromHours(10);
            var relation = new MovieRoomRelation
            {
                StartTime = reservationTime.Add(TimeSpan.FromHours(1)),
                Room = new Room { RowsOfSeats = 10, SeatsPerRow = 10 }
            };

            MovieService movieService = CreateMovieServiceOperatingOn(relation);

            // when
            using (FrozenDomainTime.At(DateTime.Today.Add(reservationTime)))
            {
                bool success = movieService.ReserveSeat(1, 1, 3, 3);
                Assert.IsTrue(success);
            }
        }

        [TestMethod]
        public void MovieService_ShouldNotAllowToReserveSeat_WhenReservationIsMadeAfterMovieStart()
        {
            // given
            var reservationTime = TimeSpan.FromHours(10);
            var relation = new MovieRoomRelation
            {
                StartTime = reservationTime.Subtract(TimeSpan.FromHours(1)),
                Room = new Room { RowsOfSeats = 10, SeatsPerRow = 10 }
            };

            MovieService movieService = CreateMovieServiceOperatingOn(relation);

            // when
            using (FrozenDomainTime.At(DateTime.Today.Add(reservationTime)))
            {
                bool success = movieService.ReserveSeat(1, 1, 3, 3);
                Assert.IsFalse(success);
            }
        }

        private static MovieService CreateMovieServiceOperatingOn(MovieRoomRelation relation)
        {
            var roomRepositoryMock = new Mock<IRoomRepository>();
            roomRepositoryMock.Setup(r => r.GetRelation(It.IsAny<int>())).Returns(relation);

            return new MovieService(
                new Mock<IMovieRepository>().Object,
                roomRepositoryMock.Object,
                new List<INotificationSender>(),
                (int x) => new AuditLogger(1, new Mock<IAuditRepository>().Object));
        }
    }
}
