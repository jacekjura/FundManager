using FundManager.DTO;
using FundManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundManager.Services
{
    public class StockService : IStockService
    {
        private List<Stock> _stocks = new List<Stock>()
        {
            new Bond { Id = 1, Price = 10, Quantity = 10 },
            new Bond { Id = 2, Price = 200, Quantity = 3 },
            new Bond { Id = 3, Price = 33, Quantity = -10 },
            new Equity { Id = 1, Price = 45, Quantity = 35 },
            new Equity { Id = 2, Price = 60, Quantity = -5 },
            new Bond { Id = 4, Price = 500, Quantity = 25 }
        }
        ;
        public Stock AddStock(StockType type, decimal price, long quantity)
        {
            var stockId = _stocks.Any(p => p.Type == type) ? _stocks.Where(p => p.Type == type).Max(p => p.Id) + 1 : 1;
            Stock stock = null;
            switch (type)
            {
                case StockType.Bond:
                    stock = new Bond { Id = stockId, Price = price, Quantity = quantity };
                    break;
                case StockType.Equity:
                    stock = new Equity { Id = stockId, Price = price, Quantity = quantity };
                    break;
                default:
                    throw new ArgumentOutOfRangeException("stockType");
            }
            _stocks.Add(stock);
            return stock;
        }

        public IEnumerable<Stock> GetStocks()
        {
            return _stocks;
        }
    }
}
