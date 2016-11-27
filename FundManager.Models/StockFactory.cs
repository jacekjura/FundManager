using System;

namespace FundManager.Models
{
    public static class StockFactory
    {
        public static Stock Create(DTO.Stock stockDto)
        {
            if (stockDto == null)
                throw new ArgumentNullException("stockDto");

            switch (stockDto.Type)
            {
                case DTO.StockType.Bond:
                    return new Bond((DTO.Bond)stockDto);
                case DTO.StockType.Equity:
                    return new Equity((DTO.Equity)stockDto);
                default:
                    throw new ArgumentOutOfRangeException("stockType");
            }
        }
    }
}
