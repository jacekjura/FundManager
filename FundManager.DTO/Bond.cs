namespace FundManager.DTO
{
    public class Bond : Stock
    {
        public override StockType Type
        {
            get
            {
                return StockType.Bond;
            }
        }
    }
}
