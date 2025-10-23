using Microsoft.EntityFrameworkCore;
using StockOne.API.Data;
using StockOne.API.Mapper;
using StockOne.API.Model.DTOs;

namespace StockOne.API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext dbContext;

        public StockRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddStockAsync(CreateStockDTO stock)
        {
            var stockModel = stock.CreateDTO();
            await dbContext.Stocks.AddAsync(stockModel);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteStockAsync(int Id)
        {
            var findStock = await dbContext.Stocks.FindAsync(Id);
            if (findStock != null)
            {
                dbContext.Stocks.Remove(findStock);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<StockDTO>> GetAllStocksAsync()
        {
            var stocks = dbContext.Stocks.Include(c => c.Comments).Select(s => s.MapToDto()).ToList();
            return stocks;
        }

        public async Task<StockDTO> GetStockByIdAsync(int id)
        {
            var stock = await dbContext.Stocks.FindAsync(id);
            var stockDto = stock?.MapToDto();
            return stockDto;
        }

        public async Task UpdateStockAsync(int Id, CreateStockDTO stock)
        {
            var find = await dbContext.Stocks.FindAsync(Id);
            if (find != null)
            {
                find.Symbol = stock.Symbol;
                find.CompanyName = stock.CompanyName;
                find.Purchase = stock.Purchase;
                find.LastDiv = stock.LastDiv;
                find.Industry = stock.Industry;
                find.MarketCap = stock.MarketCap;
                dbContext.Stocks.Update(find);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
