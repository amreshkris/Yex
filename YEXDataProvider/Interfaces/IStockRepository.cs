using System.Collections.Generic;
using YEXEntities;

namespace YEXDataProvider.Interfaces
{
    public interface IStockRepository
    {
        void LoadStock(List<StockDetail> stock);
        StockDetail GetStockDetail(string stockName);        
        List<StockDetail> GetAllStock();
        List<OrderDetail> SaveOrder(OrderDetail orderDetail);
        List<OrderDetail> GetOrderHistory();
    }
}
