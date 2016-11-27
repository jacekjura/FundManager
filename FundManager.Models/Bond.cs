namespace FundManager.Models
{
    public class Bond : Stock
    {
        private readonly decimal _transactionCosts;
        public Bond(DTO.Bond bondDTO) : base(bondDTO)
        {
            _transactionCosts = MarketValue * 0.02m;
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
                    IsHighlighted = MarketValue < 0 || TransactionCosts > 100000
                };
            }
        }
    }
}
