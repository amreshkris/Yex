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
        public HttpResponseMessage Get(string id)
        {
            var result = _stockBusiness.GetStockDetail("aaa");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        [Route("api/load/{numberOfStocks:int}")]
        public HttpResponseMessage Load([FromUri]int numberOfStocks)
        {
            var result = _stockBusiness.LoadStock(numberOfStocks);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/stock/fluctuate")]
        [HttpGet]
        public HttpResponseMessage FluctuatePrice()
        {
            var result = _stockBusiness.FluctuatePrice();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
