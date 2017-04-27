using Microsoft.Practices.Unity;
using System.Web.Http;
using YEXBusiness;
using YEXDataProvider;
using YEXDataProvider.Interfaces;
using YEXStockExchange.Controllers;

namespace YEXStockExchange.Unity
{
    public static class RegisterTypes
    {
        /// <summary>
        /// Configure Api
        /// </summary>
        /// <param name="config">Config</param>
        public static void ConfigureApi(HttpConfiguration config)
        {
            var unity = new UnityContainer();

            //Register controllers involved in IOC.
            unity.RegisterType<StockController>();
            unity.RegisterType<OrderController>();
            unity.RegisterType<IStockBusiness, StockBusiness>(new HierarchicalLifetimeManager());
            unity.RegisterType<IStockRepository, StockRepository>(new HierarchicalLifetimeManager());
           
            config.DependencyResolver = new IoCContainer(unity);

        }
    }
}