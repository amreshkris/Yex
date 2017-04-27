namespace YEXEntities
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public string StockName { get; set; }
        public int StockQuantity { get; set; }
        public decimal StockPrice { get; set; }
    }
}
