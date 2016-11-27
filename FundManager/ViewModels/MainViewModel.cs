using FundManager.Models;
using FundManager.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FundManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IStockService _stockService;
        private DTO.StockType _selectedStockType;
        private long? _quantity;
        private decimal? _price;
        private ObservableCollection<Stock> _stocks;
        private long _equityTotalNumber;
        private decimal _equityTotalStockWeight;
        private decimal _equityTotalMarketValue;
        private long _bondTotalNumber;
        private decimal _bondTotalStockWeight;
        private decimal _bondTotalMarketValue;
        private long _allTotalNumber;
        private decimal _allTotalStockWeight;
        private decimal _allTotalMarketValue;
        public MainViewModel(IStockService stockService)
        {
            _stockService = stockService;
            AddStockCommand = new RelayCommand(AddStock, CanAddStock);
            LoadStocks();
        }

        public RelayCommand AddStockCommand { get; set; }

        public DTO.StockType SelectedStockType
        {
            get { return _selectedStockType; }
            set
            {
                _selectedStockType = value;
                RaisePropertyChanged("SelectedStockType");
            }
        }
        public long? Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                AddStockCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("Quantity");
            }
        }
        public decimal? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                AddStockCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("Price");
            }
        }
        public long EquityTotalNumber
        {
            get { return _equityTotalNumber; }
            set
            {
                _equityTotalNumber = value;
                RaisePropertyChanged("EquityTotalNumber");
            }
        }
        public decimal EquityTotalStockWeight
        {
            get { return _equityTotalStockWeight; }
            set
            {
                _equityTotalStockWeight = value;
                RaisePropertyChanged("EquityTotalStockWeight");
            }
        }
        public decimal EquityTotalMarketValue
        {
            get { return _equityTotalMarketValue; }
            set
            {
                _equityTotalMarketValue = value;
                RaisePropertyChanged("EquityTotalMarketValue");
            }
        }
        public long BondTotalNumber
        {
            get { return _bondTotalNumber; }
            set
            {
                _bondTotalNumber = value;
                RaisePropertyChanged("BondTotalNumber");
            }
        }
        public decimal BondTotalStockWeight
        {
            get { return _bondTotalStockWeight; }
            set
            {
                _bondTotalStockWeight = value;
                RaisePropertyChanged("BondTotalStockWeight");
            }
        }
        public decimal BondTotalMarketValue
        {
            get { return _bondTotalMarketValue; }
            set
            {
                _bondTotalMarketValue = value;
                RaisePropertyChanged("BondTotalMarketValue");
            }
        }
        public long AllTotalNumber
        {
            get { return _allTotalNumber; }
            set
            {
                _allTotalNumber = value;
                RaisePropertyChanged("AllTotalNumber");
            }
        }
        public decimal AllTotalStockWeight
        {
            get { return _allTotalStockWeight; }
            set
            {
                _allTotalStockWeight = value;
                RaisePropertyChanged("AllTotalStockWeight");
            }
        }
        public decimal AllTotalMarketValue
        {
            get { return _allTotalMarketValue; }
            set
            {
                _allTotalMarketValue = value;
                RaisePropertyChanged("AllTotalMarketValue");
            }
        }
        public ObservableCollection<Stock> Stocks
        {
            get { return _stocks; }
            set
            {
                _stocks = value;
                RaisePropertyChanged("Stocks");
            }
        }
        private void AddStock()
        {
            if (Price == null)
            {
                throw new ArgumentNullException("Price");
            }
            if (Quantity == null)
            {
                throw new ArgumentNullException("Quantity");
            }
            var stockDTO = _stockService.AddStock(SelectedStockType, Price.Value, Quantity.Value);
            Stocks.Add(StockFactory.Create(stockDTO));
            UpdateStockWeights();
        }

        private void LoadStocks()
        {
            Stocks = new ObservableCollection<Stock>(_stockService.GetStocks().Select(p => StockFactory.Create(p)));
            UpdateStockWeights();
        }

        private void UpdateStockWeights()
        {
            var totalMarketValue = Stocks.Sum(p => p.MarketValue);
            foreach (var stock in Stocks)
            {
                stock.UpdateStockWeight(totalMarketValue);
            }

            EquityTotalMarketValue = Stocks.Where(p => p.Type == DTO.StockType.Equity).Sum(p => p.MarketValue);
            BondTotalMarketValue = Stocks.Where(p => p.Type == DTO.StockType.Bond).Sum(p => p.MarketValue);
            AllTotalMarketValue = totalMarketValue;

            EquityTotalStockWeight = Stocks.Where(p => p.Type == DTO.StockType.Equity).Sum(p => p.StockWeight);
            BondTotalStockWeight = Stocks.Where(p => p.Type == DTO.StockType.Bond).Sum(p => p.StockWeight);
            AllTotalStockWeight = Stocks.Sum(p => p.StockWeight);

            EquityTotalNumber = Stocks.Where(p => p.Type == DTO.StockType.Equity).Sum(p => p.Quantity);
            BondTotalNumber = Stocks.Where(p => p.Type == DTO.StockType.Bond).Sum(p => p.Quantity);
            AllTotalNumber = Stocks.Sum(p => p.Quantity);
        }

        public bool CanAddStock()
        {
            return Price.HasValue && Price > 0 && Quantity.HasValue && Quantity != 0;
        }
    }
}
