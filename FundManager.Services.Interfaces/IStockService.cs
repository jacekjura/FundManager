using FundManager.DTO;
using System.Collections.Generic;

namespace FundManager.Services.Interfaces
{
    public interface IStockService
    {
        Stock AddStock(StockType stockType, decimal price, long quantity);
        IEnumerable<Stock> GetStocks();
    }
}
