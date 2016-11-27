namespace FundManager.Models
{
    public class Equity : Stock
    {
        private readonly decimal _transactionCosts;
        public Equity(DTO.Equity bondDTO) : base(bondDTO)
        {
            _transactionCosts = MarketValue * 0.005m;
        }
        public override decimal TransactionCosts
        {
            get
            {
                return _transactionCosts;
            }
        }

        public override StockHighlitedName HighlightedName
        {
            get
            {
                return new StockHighlitedName
                {
                    Name = Name,
                    IsHighlighted = MarketValue < 0 || TransactionCosts > 200000
                };
            }
        }
    }
}
