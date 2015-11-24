using Microsoft.Practices.Unity;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace IoCCinema.CompositionRoot
{
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private readonly UnityContainer _container;

        public UnityControllerFactory(UnityContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (IController)_container.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
        }
    }
}