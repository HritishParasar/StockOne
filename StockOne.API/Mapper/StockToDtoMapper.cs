using StockOne.API.Model;
using StockOne.API.Model.DTOs;

namespace StockOne.API.Mapper
{
    public static class StockToDtoMapper
    {
        public static StockDTO MapToDto(this Stock stock)
        {
            return new StockDTO
            {
                Id = stock.Id,
                CompanyName = stock.CompanyName,
                Symbol = stock.Symbol,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap
            };
        }
    }
}
