using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCCinema.Business
{
    public class Seanse
    {
        public int SeanseId { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public virtual Room Room { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual List<SeatAssignment> SeatAssignments { get; set; }

        public bool HasEnded
        {
            get { return StartTime < DomainTime.Current.Now.TimeOfDay; }
        }

        internal void ReserveSeatForUser(int userId, Seat seat)
        {
            if (HasEnded)
            {
                throw new InvalidOperationException("Seanse already ended");
            }

            if (Room.HasSeat(seat))
            {
                throw new InvalidOperationException("This room doesn't have such seat");
            }

            var existingSeat = SeatAssignments
                .FirstOrDefault(a => a.Row == seat.Row && a.SeatNumber == seat.SeatNumber);

            if (existingSeat != null)
            {
                throw new InvalidOperationException("Seat already taken");
            }

            SeatAssignments.Add(new SeatAssignment
            {
                SeanseId = SeanseId,
                Row = seat.Row,
                SeatNumber = seat.SeatNumber,
                UserId = userId
            });
        }
    }
}
