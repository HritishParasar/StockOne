using StockOne.API.Model;
using StockOne.API.Model.DTOs;

namespace StockOne.API.Repository
{
    public interface IStockRepository
    {
        Task<List<StockDTO>> GetAllStocksAsync();
        Task<StockDTO> GetStockByIdAsync(int id);
        Task AddStockAsync(CreateStockDTO stock);
        Task DeleteStockAsync(int Id);
        Task UpdateStockAsync(int Id, CreateStockDTO stock);
    }
}
