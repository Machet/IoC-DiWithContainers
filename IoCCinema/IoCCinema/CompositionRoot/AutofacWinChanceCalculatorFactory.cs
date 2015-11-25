using Autofac;
using IoCCinema.Business;
using IoCCinema.Business.Lotery;
using System;

namespace IoCCinema.CompositionRoot
{
    public class AutofacWinChanceCalculatorFactory : IWinChanceCalculatorFactory
    {
        private readonly IComponentContext context;

        public AutofacWinChanceCalculatorFactory(IComponentContext context)
        {
            this.context = context;
        }

        public IWinChanceCalculator GetCalculatorForUser(User user)
        {
            return context.ResolveNamed<IWinChanceCalculator>(user.UserType.ToString());
        }
    }
}