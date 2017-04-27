using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEXEntities;

namespace YEXBusiness
{
    public interface IStockBusiness
    {
        List<StockDetail> LoadStock(int numberOfStocks);
        StockDetail GetStockDetail(string stockName);
        List<StockDetail> FluctuatePrice();
        List<OrderDetail> SaveOrder(OrderDetail orderDetail);
        List<OrderDetail> GetOrderHistory();
    }
}
