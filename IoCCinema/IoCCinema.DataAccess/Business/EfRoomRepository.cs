using System.Linq;
using IoCCinema.Business;
using System;

namespace IoCCinema.DataAccess.Business
{
    public class EfRoomRepository : IRoomRepository
    {
        private CinemaContext _context;

        public EfRoomRepository(CinemaContext context)
        {
            _context = context;
        }

        public Seanse GetSeanse(int seanseId)
        {
            var seanse = _context.Seanses
                .FirstOrDefault(r => r.SeanseId == seanseId);

            if (seanse == null)
            {
                throw new ArgumentException("Seanse doesn't exist");
            }

            return seanse;
        }
    }
}
