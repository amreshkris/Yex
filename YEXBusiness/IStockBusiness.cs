using System.Collections.Generic;
using YEXEntities;

namespace YEXBusiness
{
    public interface IStockBusiness
    {
        List<StockDetail> LoadStock(int numberOfStocks);
        StockDetail GetStockDetail(string stockName);
        List<StockDetail> FluctuatePrice();
        bool SaveOrder(OrderDetail orderDetail);
        List<OrderDetail> GetOrderHistory();
    }
}
