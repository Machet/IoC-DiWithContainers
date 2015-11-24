using IoCCinema.Controllers;
using IoCCinema.DataAccess.Presentation;
using IoCCinema.Presentation;
using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace IoCCinema.CompositionRoot
{
    public class DIConfig
    {
        public static void Setup()
        {
            var container = new UnityContainer();
            container.RegisterType<IMovieViewRepository, EfMovieViewRepository>();
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(container));
        }
    }
}