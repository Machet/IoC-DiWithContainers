using IoCCinema.Business;
using IoCCinema.Business.Lotery;
using Ninject;

namespace IoCCinema.CompositionRoot
{
    public class NinjectWinChanceCalculatorFactory : IWinChanceCalculatorFactory
    {
        private readonly IKernel _kernel;

        public NinjectWinChanceCalculatorFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IWinChanceCalculator GetCalculatorForUser(User user)
        {
            return _kernel.Get<IWinChanceCalculator>(user.UserType.ToString());
        }
    }
}