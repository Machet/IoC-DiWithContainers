using System;
using System.Collections.Generic;

namespace IoCCinema.Business
{
    public class MovieRoomRelation
    {
        public int MovieRoomRelationId { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public virtual Room Room { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual List<SeatAssignment> SeatAssignments { get; set; }
    }
}
