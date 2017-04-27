using System.ComponentModel.DataAnnotations.Schema;

namespace  YEXEntities
{
    public class StockDetail
    {
        public int StockDetailID { get; set; }
        public string StockName { get; set; }
        public decimal StockPrice { get; set; }   
        //public decimal LastDayPrice { get; set; }
        public decimal PreviousStockPrice { get; set; }
        [NotMapped]
        public decimal StockDifference { get; set; }
        [NotMapped]
        public decimal StockDifferencePercentage { get; set; }
    }
}