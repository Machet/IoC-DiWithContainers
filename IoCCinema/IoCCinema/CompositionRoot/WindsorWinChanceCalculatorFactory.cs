using Castle.MicroKernel;
using IoCCinema.Business;
using IoCCinema.Business.Lotery;

namespace IoCCinema.CompositionRoot
{
    public class WindsorWinChanceCalculatorFactory : IWinChanceCalculatorFactory
    {
        private readonly IKernel _kernel;

        public WindsorWinChanceCalculatorFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IWinChanceCalculator GetCalculatorForUser(User user)
        {
            return (IWinChanceCalculator)_kernel.Resolve(user.UserType.ToString(), typeof(IWinChanceCalculator));
        }
    }
}