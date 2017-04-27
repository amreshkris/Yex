using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using YEXBusiness;
using YEXEntities;

namespace YEXStockExchange.Controllers
{
    [EnableCors("*", "*", "*")]
    public class OrderController : ApiController
    {
        private readonly IStockBusiness _stockBusiness;
        public OrderController(IStockBusiness stockBusiness)
        {
            _stockBusiness = stockBusiness;
        }

        /// <summary>
        /// SaveOrder - Save order details
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [Route("api/order")]
        [HttpPost]
        public HttpResponseMessage SaveOrder([FromBody] OrderDetail order)
        {
            var result = _stockBusiness.SaveOrder(order);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get Order history
        /// </summary>
        /// <returns></returns>
        [Route("api/order")]
        [HttpGet]
        public HttpResponseMessage GetOrder()
        {
            var result = _stockBusiness.GetOrderHistory();
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
