using Castle.Windsor;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace IoCCinema.CompositionRoot
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            Type controllerType = GetControllerType(requestContext, controllerName);
            return (IController)_container.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller);
        }
    }
}