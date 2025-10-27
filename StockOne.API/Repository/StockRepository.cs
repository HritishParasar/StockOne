using Microsoft.EntityFrameworkCore;
using StockOne.API.Data;
using StockOne.API.FilteringSorting;
using StockOne.API.Mapper;
using StockOne.API.Model;
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

        public async Task<List<Stock>> GetAllStocksAsync(QueryObject query)
        {
            var stocks = dbContext.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrEmpty(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            if(!string.IsNullOrEmpty(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                switch(query.SortBy.ToLower())
                {
                    case "symbol":
                        stocks = query.IsSortAscending ? stocks.OrderBy(s => s.Symbol) : stocks.OrderByDescending(s => s.Symbol);
                        break;
                    case "companyname":
                        stocks = query.IsSortAscending ? stocks.OrderBy(s => s.CompanyName) : stocks.OrderByDescending(s => s.CompanyName);
                        break;
                    case "purchase":
                        stocks = query.IsSortAscending ? stocks.OrderBy(s => s.Purchase) : stocks.OrderByDescending(s => s.Purchase);
                        break;
                    case "marketcap":
                        stocks = query.IsSortAscending ? stocks.OrderBy(s => s.MarketCap) : stocks.OrderByDescending(s => s.MarketCap);
                        break;
                    default:
                        stocks = stocks.OrderBy(s => s.Id);
                        break;
                }
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
            stocks = stocks.Skip(skip).Take(query.PageSize);

            return await stocks.ToListAsync();
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
