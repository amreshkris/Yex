using System;
using System.Collections.Generic;
using System.Linq;
using YEXDataProvider.Interfaces;
using YEXEntities;

namespace YEXBusiness
{
    public class StockBusiness : IStockBusiness
    {
        private readonly IStockRepository _stockRepository;

        public StockBusiness(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        /// <summary>
        /// FluctuatePrice
        /// </summary>
        /// <returns></returns>
        public List<StockDetail> FluctuatePrice()
        {
            var randomPriceGenerator = new Random();
            decimal stockPrice;
            decimal stockDifference;
            decimal stockDifferencePercentage;
            var currentStocks = _stockRepository.GetAllStock();
            if (currentStocks.Any())
            {
                foreach (var stock in currentStocks)
                {
                    stockPrice = stock.PreviousStockPrice +
                                 GetNegativeRandomPrice(randomPriceGenerator)*stock.PreviousStockPrice;
                    ;
                    stock.StockPrice = Math.Round(stockPrice, 2, MidpointRounding.AwayFromZero);
                    stockDifference = stock.StockPrice > stock.PreviousStockPrice
                        ? stock.StockPrice - stock.PreviousStockPrice
                        : (stock.PreviousStockPrice - stock.StockPrice)*-1;
                    stock.StockDifference = stockDifference;
                    stockDifferencePercentage = stockDifference > 0
                        ? (stockDifference/stock.PreviousStockPrice)*100
                        : ((stockDifference/stock.PreviousStockPrice)*100)*-1;
                    stock.StockDifferencePercentage =
                        Math.Round(
                            (stockDifference > 0
                                ? (stockDifference/stock.PreviousStockPrice)*100
                                : Math.Abs(((stockDifference/stock.PreviousStockPrice)*100))*-1), 2,
                            MidpointRounding.AwayFromZero);
                }
            }
            return currentStocks;
        }

        /// <summary>
        /// GetStockDetail
        /// </summary>
        /// <param name="stockName"></param>
        /// <returns></returns>
        public StockDetail GetStockDetail(string stockName)
        {
            if (string.IsNullOrWhiteSpace(stockName))
            {
                throw new ArgumentNullException("stockName", "StockName is not valid");
            }
            var result = this._stockRepository.GetStockDetail(stockName);
            return result;
        }

        /// <summary>
        /// LoadStock
        /// </summary>
        /// <param name="numberOfStocks"></param>
        /// <returns></returns>
        public List<StockDetail> LoadStock(int numberOfStocks)
        {
            var sampleData = _stockRepository.GetAllStock();
            if (!sampleData.Any())
            {
                var randomPriceGenerator = new Random();
                for (int stockNumber = 0; stockNumber < numberOfStocks; stockNumber++)
                {
                    var previousPrice = GetRandomPrice(randomPriceGenerator, 200, 2);
                    previousPrice = Math.Round(previousPrice, 2, MidpointRounding.AwayFromZero);
                    var currentPrice = previousPrice + GetNegativeRandomPrice(randomPriceGenerator)*previousPrice;
                    currentPrice = Math.Round(currentPrice, 2, MidpointRounding.AwayFromZero);
                    var stockDifference = currentPrice > previousPrice
                        ? Math.Abs(currentPrice) - Math.Abs(previousPrice)
                        : (Math.Abs(previousPrice) - Math.Abs(currentPrice))*-1;
                    var stockDetail = new StockDetail()
                    {
                        StockDetailID = stockNumber,
                        StockName =
                            stockNumber == 0
                                ? GenerateStockName(null)
                                : GenerateStockName(sampleData[stockNumber - 1].StockName),
                        PreviousStockPrice = previousPrice,
                        StockPrice = currentPrice,
                        StockDifference = Math.Round(stockDifference, 2, MidpointRounding.AwayFromZero),
                        StockDifferencePercentage =
                            Math.Round(
                            (stockDifference > 0
                                ? (stockDifference/previousPrice)*100
                                : Math.Abs(((stockDifference/previousPrice)*100))*-1), 2, MidpointRounding.AwayFromZero)
                    };
                    sampleData.Add(stockDetail);
                }
                _stockRepository.LoadStock(sampleData);
            }
            return sampleData;
        }

        /// <summary>
        /// SaveOrder
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <returns></returns>
        public bool SaveOrder(OrderDetail orderDetail)
        {
            var result = false;
            if (orderDetail == null)
            {
                throw new ArgumentNullException("orderDetail", "Invalid order detail");
            }
            try
            {
                _stockRepository.SaveOrder(orderDetail);
                result = true;
            }
            catch (Exception ex)
            {
                //log 
            }
            return result;
        }

        /// <summary>
        /// GetOrderHistory
        /// </summary>
        /// <returns></returns>
        List<OrderDetail> IStockBusiness.GetOrderHistory()
        {
            List<OrderDetail> result;
            var orderHistory = new List<OrderDetail>();
            try
            {
                orderHistory = _stockRepository.GetOrderHistory();
                result = new List<OrderDetail>();
            }
            catch (Exception ex)
            {
               // log
                result = null;
            }
            if (orderHistory!=null && orderHistory.Any())
            {
                result = orderHistory
                       .GroupBy(a => a.StockName)
                       .Select(order => new OrderDetail
                       {
                           StockName = order.First().StockName,
                           StockQuantity = order.Sum(c => c.StockQuantity)
                       }).ToList<OrderDetail>();
            }           
            return result;
        }

        #region private method
        private decimal GetRandomPrice(Random randomPriceGenerator, double maximum, double minimum)
        {
            return Convert.ToDecimal(randomPriceGenerator.NextDouble() * (maximum - minimum));
        }

        private decimal GetNegativeRandomPrice(Random randomPriceGenerator)
        {
            var localRandom = randomPriceGenerator.NextDouble();
            if (localRandom < 0.5)
            {
                localRandom = (localRandom / 10) * -1;
            }
            else
            {
                localRandom = (localRandom / 10);
            }
            return Convert.ToDecimal(localRandom);
        }

        private string GenerateStockName(string previousStockName)
        {
            string currentStockName;
            if (string.IsNullOrWhiteSpace(previousStockName))
            {
                currentStockName = "aaa";
            }
            else
            {
                char[] eachChars = previousStockName.ToCharArray();
                int positionToIncrement = previousStockName.IndexOf(eachChars.FirstOrDefault(x => x.Equals('z')));
                int charPositionToIncrement = 0;
                if (positionToIncrement == -1)
                {
                    charPositionToIncrement = 2;
                }
                else if (positionToIncrement == 2)
                {
                    charPositionToIncrement = 1;
                }
                else if (positionToIncrement == 1)
                {
                    charPositionToIncrement = 0;
                }
                else if (positionToIncrement == 0)
                {
                    currentStockName = "aaaa";
                }
                eachChars[charPositionToIncrement] = (char)(eachChars[charPositionToIncrement] + 1);
                currentStockName = new string(eachChars);
            }
            return currentStockName;
        }
        #endregion
    }
}
