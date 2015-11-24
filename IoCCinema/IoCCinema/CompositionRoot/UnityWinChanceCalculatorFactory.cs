using IoCCinema.Business;
using IoCCinema.Business.Lotery;
using Microsoft.Practices.Unity;

namespace IoCCinema.CompositionRoot
{
    public class UnityWinChanceCalculatorFactory : IWinChanceCalculatorFactory
    {
        private readonly IUnityContainer _container;

        public UnityWinChanceCalculatorFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IWinChanceCalculator GetCalculatorForUser(User user)
        {
            return _container.Resolve<IWinChanceCalculator>(user.UserType.ToString());
        }
    }
}