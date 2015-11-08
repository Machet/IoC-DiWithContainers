using System;
using System.Collections.Generic;
using IoCCinema.Business.AuditLogging;
using IoCCinema.Business.DTO;
using IoCCinema.Business.Notifications;

namespace IoCCinema.Business
{
    public class MovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly List<INotificationSender> _notifiers;
        private readonly Func<int, AuditLogger> _loggerFactory;

        public MovieService(
            IMovieRepository movieRepository,
            IRoomRepository roomRepository,
            List<INotificationSender> notifiers,
            Func<int, AuditLogger> loggerFactoryFn)
        {
            _movieRepository = movieRepository;
            _roomRepository = roomRepository;
            _notifiers = notifiers;
            _loggerFactory = loggerFactoryFn;
        }

        public List<MovieDTO> GetMovies(DateTime start)
        {
            return _movieRepository.GetMovies(start);
        }

        public RoomDTO GetRoomByRelation(int movieRoomRelationId)
        {
            return _roomRepository.GetRoomByRelation(movieRoomRelationId);
        }

        public bool ReserveSeat(int userId, int movieRoomRelationId, int row, int seatNumber)
        {
            MovieRoomRelation relation = _roomRepository.GetRelation(movieRoomRelationId);

            if (relation == null)
            {
                return false;
            }

            if (relation.StartTime < DomainTime.Current.Now.TimeOfDay)
            {
                return false;
            }

            if (relation.Room.SeatsPerRow < row || row <= 0)
            {
                return false;
            }

            if (relation.Room.SeatsPerRow < seatNumber || seatNumber <= 0)
            {
                return false;
            }

            if (_roomRepository.GetSeatAssignment(movieRoomRelationId, row, seatNumber) != null)
            {
                return false;
            }

            _roomRepository.Add(new SeatAssignment
            {
                MovieRoomRelationId = movieRoomRelationId,
                Row = row,
                SeatNumber = seatNumber,
                UserId = userId
            });

            foreach (var notifier in _notifiers)
            {
                try
                {
                    notifier.NotifyReservationReady(userId, row, seatNumber);
                }
                catch (Exception)
                {
                    // log 
                }
            }

            _loggerFactory(userId).LogChanges(string.Format("Booked seat {0} in row {1}", seatNumber, row));

            return true;
        }
    }
}
