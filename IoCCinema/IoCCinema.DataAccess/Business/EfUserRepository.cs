﻿using IoCCinema.Business;
using System.Linq;

namespace IoCCinema.DataAccess.Business
{
    public class EfUserRepository : IUserRepository
    {
        private readonly CinemaContext _context;

        public EfUserRepository(CinemaContext context)
        {
            _context = context;
        }

        public int GetReservationsCountForUser(int userId)
        {
            return _context.SeatAssignments.Count(s => s.UserId == userId);
        }

        public User GetUser(int userId)
        {
            return _context.Users.Find(userId);
        }
    }
}
