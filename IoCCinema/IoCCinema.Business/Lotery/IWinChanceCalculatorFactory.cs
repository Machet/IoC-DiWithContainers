namespace IoCCinema.Business.Lotery
{
    /// <summary>
    /// creates win chance calculator for specific user 
    /// </summary>
    public interface IWinChanceCalculatorFactory
    {
        IWinChanceCalculator GetCalculatorForUser(User user);
    }
}
