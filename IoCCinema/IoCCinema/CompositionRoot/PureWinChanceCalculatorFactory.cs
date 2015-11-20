using IoCCinema.Business;
using IoCCinema.Business.Lotery;
using System;

namespace IoCCinema.CompositionRoot
{
    public class PureWinChanceCalculatorFactory : IWinChanceCalculatorFactory
    {
        private readonly IUserRepository _userRepository;

        public PureWinChanceCalculatorFactory(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IWinChanceCalculator GetCalculatorForUser(User user)
        {
            switch (user.UserType)
            {
                case UserType.Regular:
                    return new RegularUserWinChanceCalculator();
                case UserType.Silver:
                    return new SilverUserWinChanceCalculator(_userRepository);
                case UserType.Gold:
                    return new GoldUserWinChanceCalculator(_userRepository);
                default:
                    throw new ArgumentException("Could not find calculator for type " + user.UserType);
            }
        }
    }
}