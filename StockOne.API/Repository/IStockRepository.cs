using StockOne.API.FilteringSorting;
using StockOne.API.Model;
using StockOne.API.Model.DTOs;

namespace StockOne.API.Repository
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync(QueryObject query);
        Task<StockDTO> GetStockByIdAsync(int id);
        Task AddStockAsync(CreateStockDTO stock);
        Task DeleteStockAsync(int Id);
        Task UpdateStockAsync(int Id, CreateStockDTO stock);
    }
}
