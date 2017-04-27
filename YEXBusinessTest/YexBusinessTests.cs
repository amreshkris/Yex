using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YEXBusiness;
using YEXDataProvider.Interfaces;
using YEXDataProvider.Interfaces.Fakes;
using YEXEntities;
using System.Collections.Generic;

namespace YEXBusinessTest
{
    [TestClass]
    public class YexBusinessTests
    {
        [TestMethod]
        public void FluctuatePrice_NullStocks()
        {
            IStockRepository stockRepository = new StubIStockRepository()
            {
                GetAllStock = () => new List<StockDetail>()
            };
            var result = new StockBusiness(stockRepository).FluctuatePrice();
            Assert.IsInstanceOfType(result,typeof(List<StockDetail>));
        }

        [TestMethod]
        public void FluctuatePrice_ValidResult()
        {
            IStockRepository stockRepository = new StubIStockRepository()
            {
                GetAllStock = () => new List<StockDetail>()
                {
                    new StockDetail()
                    {
                        PreviousStockPrice =  1.0M,
                        StockName = "aaa",
                        StockDetailID = 1,
                        StockDifference = 2.0M,
                        StockDifferencePercentage = -0.1M,
                        StockPrice = 10.0M
                    }
                }
            };
            var result = new StockBusiness(stockRepository).FluctuatePrice();
            Assert.IsInstanceOfType(result, typeof(List<StockDetail>));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetStockDetail_Success()
        {
            IStockRepository stockRepository = new StubIStockRepository()
            {
                GetStockDetailString = (stockname) => new StockDetail()
                {
                    StockName =  "test",
                    StockPrice = 1,
                    PreviousStockPrice = 2.0M
                }
            };
            Assert.IsNotNull(new StockBusiness(stockRepository).GetStockDetail("test"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetStockDetail_NoData()
        {
            IStockRepository stockRepository = new StubIStockRepository()
            {
                GetStockDetailString = (stockname) => new StockDetail()
                {
                    StockName =  "test",
                    StockPrice = 1,
                    PreviousStockPrice = 2.0M
                }
            };
            new StockBusiness(stockRepository).GetStockDetail("");
        }

        [TestMethod]
        public void LoadStock_NoData()
        {
            IStockRepository stockRepository = new StubIStockRepository()
            {
                GetAllStock = () => new List<StockDetail>()
            };
            var result = new StockBusiness(stockRepository).LoadStock(2);
            Assert.IsTrue(result.Count == 2);
        }

        public void LoadStock_Success()
        {
            IStockRepository stockRepository = new StubIStockRepository()
            {
                GetAllStock = () => new List<StockDetail>()
                {
                    new StockDetail()
                    {
                        StockDetailID = 1,
                        StockName = "aaa"
                    }
                }
            };
            var result = new StockBusiness(stockRepository).LoadStock(2);
            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SaveOrder_InvalidInput()
        {
            IStockRepository stockRepository = new StubIStockRepository()
            {
                SaveOrderOrderDetail = (orderdetail) => { }
            };
            var result = new StockBusiness(stockRepository).SaveOrder(null);
        }
    }
}
