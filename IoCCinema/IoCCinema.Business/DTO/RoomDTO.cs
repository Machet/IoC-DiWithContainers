﻿using System.Collections.Generic;

namespace IoCCinema.Business.DTO
{
    public class RoomDTO
    {
        public int SeatsPerRow { get; set; }
        public int RowsOfSeats { get; set; }
        public List<SeatDTO> TakenSeats { get; set; }
    }
}
