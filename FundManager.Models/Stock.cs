using GalaSoft.MvvmLight;

namespace FundManager.Models
{
    public abstract class Stock : ObservableObject
    {
        protected readonly DTO.Stock _stockDTO;
        private readonly decimal _marketValue;
        private decimal _stockWeight;

        public Stock(DTO.Stock stockDTO)
        {
            _stockDTO = stockDTO;
            _marketValue = _stockDTO.Price * _stockDTO.Quantity;
        }

        public DTO.StockType Type
        {
            get { return _stockDTO.Type; }
        }

        public decimal Price
        {
            get { return _stockDTO.Price; }
        }

        public long Quantity
        {
            get { return _stockDTO.Quantity; }
        }

        public decimal MarketValue
        {
            get { return _marketValue; }
        }

        public virtual decimal TransactionCosts { get; }

        public decimal StockWeight
        {
            get
            {
                return _stockWeight;
            }
            set
            {
                _stockWeight = value;
                RaisePropertyChanged("StockWeight");
            }
        }

        public virtual StockHighlitedName HighlightedName { get; }

        protected string Name
        {
            get { return _stockDTO.Type.ToString() + _stockDTO.Id; }
        }

        public void UpdateStockWeight(decimal totalMarketValue)
        {
            StockWeight = totalMarketValue == 0 ? 0 : _marketValue / totalMarketValue;
        }
    }
}
