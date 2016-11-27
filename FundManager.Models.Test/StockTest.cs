using FluentAssertions;
using NUnit.Framework;

namespace FundManager.Models.Test
{
    [TestFixture]
    public class StockTest
    {
        [Test]
        [TestCase(1, 10, 20)]
        [TestCase(2, 10, -20)]
        [TestCase(3, 3000, 1667)]
        public void Bond_Constructor_CorrectProperties(int id, decimal price, int quantity)
        {
            var subject = new Bond(new DTO.Bond { Id = id, Price = price, Quantity = quantity });
            subject.Price.Should().Be(price);
            subject.Quantity.Should().Be(quantity);
            subject.MarketValue.Should().Be(price * quantity);
            subject.TransactionCosts.Should().Be(subject.MarketValue * 0.02m);
            subject.Type.Should().Be(DTO.StockType.Bond);
            subject.HighlightedName.Name.Should().Be("Bond"+id);
            subject.HighlightedName.IsHighlighted.Should().Be(subject.MarketValue < 0 || subject.TransactionCosts > 100000);
        }

        [Test]
        [TestCase(1, 10, 20, 3000)]
        [TestCase(2, 10, -20, 2000)]
        public void Bond_UpdateStockWeight__CorrectStockWeight(int id, decimal price, int quantity, decimal totalMarketValue)
        {
            var subject = new Bond(new DTO.Bond { Id = id, Price = price, Quantity = quantity });
            subject.UpdateStockWeight(totalMarketValue);
            subject.StockWeight.Should().Be(subject.MarketValue / totalMarketValue);
        }

        [Test]
        [TestCase(1, 10, 20)]
        [TestCase(2, 10, -20)]
        [TestCase(3, 5000, 8001)]
        public void Equity_Constructor_CorrectProperties(int id, decimal price, int quantity)
        {
            var subject = new Equity(new DTO.Equity { Id = id, Price = price, Quantity = quantity });
            subject.Price.Should().Be(price);
            subject.Quantity.Should().Be(quantity);
            subject.MarketValue.Should().Be(price * quantity);
            subject.TransactionCosts.Should().Be(subject.MarketValue * 0.005m);
            subject.Type.Should().Be(DTO.StockType.Equity);
            subject.HighlightedName.Name.Should().Be("Equity" + id);
            subject.HighlightedName.IsHighlighted.Should().Be(subject.MarketValue < 0 || subject.TransactionCosts > 200000);
        }

        [Test]
        [TestCase(1, 10, 20, 3000)]
        [TestCase(2, 10, -20, 2000)]
        public void Equity_UpdateStockWeight_CorrectStockWeight(int id, decimal price, int quantity, decimal totalMarketValue)
        {
            var subject = new Equity(new DTO.Equity { Id = id, Price = price, Quantity = quantity });
            subject.UpdateStockWeight(totalMarketValue);
            subject.StockWeight.Should().Be(subject.MarketValue / totalMarketValue);
        }
    }
}