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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
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