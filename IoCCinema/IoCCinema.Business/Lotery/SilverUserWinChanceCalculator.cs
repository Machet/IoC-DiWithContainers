using System;

namespace IoCCinema.Business.Lotery
{
    public class SilverUserWinChanceCalculator : IWinChanceCalculator
    {
        private readonly IUserRepository _userRepository;

        public SilverUserWinChanceCalculator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public WinChance CalculateWinChance(User user)
        {
            if(user.FreeTicketsCount > 2)
            {
                return new WinChance(5);
            }

            int reservationsMade = _userRepository.GetReservationsCountForUser(user.Id);
            return new WinChance(10 + 5 * reservationsMade);
        }
    }
}
