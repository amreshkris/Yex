using System.Net;
using System.Net.Http;
using System.Web.Http;
using YEXBusiness;
using YEXEntities;

namespace YEXStockExchange.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IStockBusiness _stockBusiness;
        public OrderController(IStockBusiness stockBusiness)
        {
            _stockBusiness = stockBusiness;
        }

        [Route("api/order")]
        [HttpPost]
        public HttpResponseMessage SaveOrder([FromBody] OrderDetail order)
        {
            var result = _stockBusiness.SaveOrder(order);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/order")]
        [HttpGet]
        public HttpResponseMessage GetOrder()
        {
            var result = _stockBusiness.GetOrderHistory();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
    
}
