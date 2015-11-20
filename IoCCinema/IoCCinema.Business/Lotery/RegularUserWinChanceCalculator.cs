namespace IoCCinema.Business.Lotery
{
    public class RegularUserWinChanceCalculator : IWinChanceCalculator
    {
        public WinChance CalculateWinChance(User user)
        {
            return new WinChance(10);
        }
    }
}
