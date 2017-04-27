using System.Net;
using System.Net.Http;
using System.Web.Http;
using YEXBusiness;
using System.Web.Http.Cors;

namespace YEXStockExchange.Controllers
{
    [EnableCors("*","*","*")]
    public class StockController : ApiController
    {
        private readonly IStockBusiness _stockBusiness;
        public StockController(IStockBusiness stockBusiness)
        {
            _stockBusiness = stockBusiness;
        }
        
        // GET: api/Stock/5
        /// <summary>
        /// Get stock detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string id)
        {
            var result = _stockBusiness.GetStockDetail("aaa");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// Load - Initial Method to create n number of stocks
        /// </summary>
        /// <param name="numberOfStocks"></param>
        /// <returns></returns>
        [Route("api/load/{numberOfStocks:int}")]
        public HttpResponseMessage Load([FromUri]int numberOfStocks)
        {
            var result = _stockBusiness.LoadStock(numberOfStocks);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// Get- Random stock data
        /// </summary>
        /// <returns></returns>
        [Route("api/stock/fluctuate")]
        [HttpGet]
        public HttpResponseMessage FluctuatePrice()
        {
            var result = _stockBusiness.FluctuatePrice();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
