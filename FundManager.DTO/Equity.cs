namespace FundManager.DTO
{
    public class Equity : Stock
    {
        public override StockType Type
        {
            get
            {
                return StockType.Equity;
            }
        }
    }
}
