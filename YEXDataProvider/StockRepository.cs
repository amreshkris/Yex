using System;
using System.Collections.Generic;
using System.Linq;
using YEXDataProvider.Interfaces;
using YEXEntities;

namespace YEXDataProvider
{
    public class StockRepository : IStockRepository
    {
        StockDetail IStockRepository.GetStockDetail(string stockName)
        {
            StockDetail result = null;
            using (YEXDBContext context = new YEXDBContext())
            {
                if (context.stocks.Any())
                {
                    result = context.stocks.FirstOrDefault(a => a.StockName.Equals(stockName));
                }
            }
            return result;
        }

        void IStockRepository.LoadStock(List<StockDetail> stock)
        {
            using (YEXDBContext context = new YEXDBContext())
            {
                foreach (var eachStock in stock)
                {
                    context.stocks.Add(eachStock);
                }
                context.SaveChanges();
            }
        }

        List<StockDetail> IStockRepository.GetAllStock()
        {
            List<StockDetail> sampleData = new List<StockDetail>();
            try
            {
                using (YEXDBContext context = new YEXDBContext())
                {
                    sampleData = context.stocks.ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return sampleData;
        }

        void IStockRepository.SaveOrder(OrderDetail orderDetail)
        {
            try
            {
                using (YEXDBContext context = new YEXDBContext())
                {
                    context.orderHistory.Add(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // handle log
            }
        }

        List<OrderDetail> IStockRepository.GetOrderHistory()
        {
            List<OrderDetail> orderHistory;
            using (YEXDBContext context = new YEXDBContext())
            {
                orderHistory = context.orderHistory.ToList();
            }
            return orderHistory;
        }
    }
}