using FluentAssertions;
using NUnit.Framework;

namespace FundManager.Models.Test
{
    [TestFixture]
    public class StockFactoryTest
    {
        [Test]
        public void Create_Bond_InstanceTypeCorrect()
        {
            var subject = StockFactory.Create(new DTO.Bond());
            subject.Should().BeOfType<Bond>();
        }

        [Test]
        public void Create_Equity_InstanceTypeCorrect()
        {
            var subject = StockFactory.Create(new DTO.Equity());
            subject.Should().BeOfType<Equity>();
        }
    }
}
