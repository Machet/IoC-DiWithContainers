using System;

namespace IoCCinema.Business.Lotery
{
    // performs lotery for single user
    internal class FreeTicketLotery
    {
        private readonly IWinChanceCalculatorFactory _calculatorFactory;

        public FreeTicketLotery(IWinChanceCalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public void DrawFreeTicketForUser(User user)
        {
            var random = new Random(); // should be injected as service to make class testable
            IWinChanceCalculator calculator = _calculatorFactory.GetCalculatorForUser(user);
            WinChance chance = calculator.CalculateWinChance(user);
            Draw draw = new Draw(random.Next(100));

            if (chance.IsSatisfiedBy(draw))
            {
                user.FreeTicketsCount++;
                DomainEventBus.Current.Raise(new FreeTicketGranted(user.Id, user.FreeTicketsCount));
            }
        }
    }
}
