using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using NextItemBuy.Services.Implementation;
using NextItemBuy.Services.Interfaces;
using System.Web.Mvc;
using Unity.Mvc5;

namespace NextItemBuy.Web.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IItemsService, ItemsService>();
            container.RegisterType<IBankService, BankService>();
            container.RegisterType<IStatisticsService, StatisticsService>();
        }
    }
}