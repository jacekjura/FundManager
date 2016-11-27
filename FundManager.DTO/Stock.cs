namespace FundManager.DTO
{
    public abstract class Stock
    {
        public int Id { get; set; }
        public virtual StockType Type { get; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
    }
}
