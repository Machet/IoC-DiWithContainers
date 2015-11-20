namespace IoCCinema.Business.Lotery
{
    public class GoldUserWinChanceCalculator : IWinChanceCalculator
    {
        private readonly IUserRepository _userRepository;

        public GoldUserWinChanceCalculator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public WinChance CalculateWinChance(User user)
        {
            int reservationsMade = _userRepository.GetReservationsCountForUser(user.Id);
            return new WinChance(50 + 5 * reservationsMade - 5 * user.FreeTicketsCount);
        }
    }
}
