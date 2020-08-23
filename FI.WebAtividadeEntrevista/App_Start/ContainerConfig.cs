using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DAL;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace WebAtividadeEntrevista
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Options.ConstructorResolutionBehavior = new IndifferentConstructor();
            Register(container);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            try
            {
                container.Verify();
            }
            catch
            {
                // ignored
            }

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void Register(Container container)
        {
            container.Register<HttpConfiguration>(Lifestyle.Singleton);
            container.Register<HttpRouteCollection>(Lifestyle.Singleton);
            container.Register<DaoCliente>(Lifestyle.Singleton);
            container.Register<DaoBeneficiario>(Lifestyle.Singleton);
        }
    }
}