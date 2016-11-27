using FluentAssertions;
using FundManager.Services.Interfaces;
using FundManager.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FundManager.ViewModel.Test
{
    [TestFixture]
    public class MainViewModelTest
    {
        private Mock<IStockService> _stockServiceMock;
        private List<DTO.Stock> _stocks;
        private MainViewModel _subject;
        [SetUp]
        public void SetUp()
        {
            _stocks = new List<DTO.Stock>()
                {
                    new DTO.Bond { Id = 1, Price = 10, Quantity = 10 },
                    new DTO.Bond { Id = 2, Price = 200, Quantity = 3 },
                    new DTO.Bond { Id = 3, Price = 33, Quantity = -10 },
                    new DTO.Equity { Id = 1, Price = 45, Quantity = 35 },
                    new DTO.Equity { Id = 2, Price = 60, Quantity = -5 },
                    new DTO.Bond { Id = 4, Price = 500, Quantity = 25 }
                };
            _stockServiceMock = new Mock<IStockService>();
            _stockServiceMock.Setup(stockService => stockService.AddStock(DTO.StockType.Bond, It.IsAny<decimal>(), It.IsAny<long>()))
                .Returns((DTO.StockType stockType, decimal price, long quantity) => new DTO.Bond { Id = 1, Price = price, Quantity = quantity });
            _stockServiceMock.Setup(stockService => stockService.AddStock(DTO.StockType.Equity, It.IsAny<decimal>(), It.IsAny<long>()))
                .Returns((DTO.StockType stockType, decimal price, long quantity) => new DTO.Equity { Id = 1, Price = price, Quantity = quantity });
            _stockServiceMock.Setup(stockService => stockService.GetStocks())
                .Returns(_stocks);
            _subject = new MainViewModel(_stockServiceMock.Object);
        }

        [Test]
        public void Constructor_LoadedStocks()
        {
            _subject.Stocks.Count.Should().Be(_stocks.Count);
            for (int stockId = 0; stockId < _stocks.Count; stockId++)
            {
                _subject.Stocks[stockId].Price.Should().Be(_stocks[stockId].Price);
                _subject.Stocks[stockId].Quantity.Should().Be(_stocks[stockId].Quantity);
            }
        }

        [Test]
        public void Constructor_EquityTotalMarketValue_Correct()
        {
            _subject.EquityTotalMarketValue.Should().Be(_subject.Stocks.Where(p => p.Type == DTO.StockType.Equity).Sum(p => p.MarketValue));
        }

        [Test]
        public void Constructor_EquityTotalNumber_Correct()
        {
            _subject.EquityTotalNumber.Should().Be(_subject.Stocks.Where(p => p.Type == DTO.StockType.Equity).Sum(p => p.Quantity));
        }

        [Test]
        public void Constructor_EquityTotalStockWeight_Correct()
        {
            _subject.EquityTotalStockWeight.Should().Be(_subject.Stocks.Where(p => p.Type == DTO.StockType.Equity).Sum(p => p.StockWeight));
        }

        [Test]
        public void Constructor_BondTotalMarketValue_Correct()
        {
            _subject.BondTotalMarketValue.Should().Be(_subject.Stocks.Where(p => p.Type == DTO.StockType.Bond).Sum(p => p.MarketValue));
        }

        [Test]
        public void Constructor_BondTotalNumber_Correct()
        {
            _subject.BondTotalNumber.Should().Be(_subject.Stocks.Where(p => p.Type == DTO.StockType.Bond).Sum(p => p.Quantity));
        }

        [Test]
        public void Constructor_BondTotalStockWeight_Correct()
        {
            _subject.BondTotalStockWeight.Should().Be(_subject.Stocks.Where(p => p.Type == DTO.StockType.Bond).Sum(p => p.StockWeight));
        }
        [Test]
        public void Constructor_AllTotalMarketValue_Correct()
        {
            _subject.AllTotalMarketValue.Should().Be(_subject.Stocks.Sum(p => p.MarketValue));
        }

        [Test]
        public void Constructor_AllTotalNumber_Correct()
        {
            _subject.AllTotalNumber.Should().Be(_subject.Stocks.Sum(p => p.Quantity));
        }

        [Test]
        public void Constructor_AllTotalStockWeight_Correct()
        {
            _subject.AllTotalStockWeight.Should().Be(_subject.Stocks.Sum(p => p.StockWeight));
        }

        [Test]
        [TestCase(DTO.StockType.Equity, 10, 20)]
        [TestCase(DTO.StockType.Bond, 5, 66)]
        [TestCase(DTO.StockType.Equity, 300, -20)]
        [TestCase(DTO.StockType.Bond, 400, -20)]
        public void ExecuteAddStockCommand_SimpleValues_StockAdded(DTO.StockType stockType, decimal price, long quantity)
        {
            _subject.SelectedStockType = stockType;
            _subject.Price = price;
            _subject.Quantity = quantity;
            _subject.AddStockCommand.Execute(null);
            var addedStock = _subject.Stocks[_subject.Stocks.Count - 1];
            addedStock.Type.Should().Be(stockType);
            addedStock.Price.Should().Be(price);
            addedStock.Quantity.Should().Be(quantity);
        }
    }
}
