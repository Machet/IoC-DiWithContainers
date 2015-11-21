using IoCCinema.Business;
using IoCCinema.Business.Lotery;
using SimpleInjector;
using System;

namespace IoCCinema.CompositionRoot
{
    public class SimpleInjectorWinChanceCalculatorFactory : IWinChanceCalculatorFactory
    {
        private readonly Container _container;

        public SimpleInjectorWinChanceCalculatorFactory(Container container)
        {
            _container = container;
        }

        public IWinChanceCalculator GetCalculatorForUser(User user)
        {
            switch (user.UserType)
            {
                case UserType.Regular:
                    return _container.GetInstance<RegularUserWinChanceCalculator>();
                case UserType.Silver:
                    return _container.GetInstance<SilverUserWinChanceCalculator>();
                case UserType.Gold:
                    return _container.GetInstance<GoldUserWinChanceCalculator>();
                default:
                    throw new ArgumentException("Unknown user type: " + user.UserType);
            }
        }
    }
}