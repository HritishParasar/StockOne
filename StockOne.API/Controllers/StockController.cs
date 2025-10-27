using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockOne.API.Data;
using StockOne.API.FilteringSorting;
using StockOne.API.Mapper;
using StockOne.API.Model.DTOs;
using StockOne.API.Repository;

namespace StockOne.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            this.stockRepository = stockRepository;
        }
        [HttpGet("GetAllStocks")]
        public async Task<IActionResult> GetAllStocks([FromQuery] QueryObject query)
        {
            try
            {
                var stocks = await stockRepository.GetAllStocksAsync(query);
                var stocksDto = stocks.Select(s => s.MapToDto());
                return Ok(stocksDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }
        [HttpGet("GetStockById/{id:int}")]
        public async Task<IActionResult> GetStocksByID(int id)
        {
            try
            {
                var find = await stockRepository.GetStockByIdAsync(id);
                if (find is null)
                {
                    return NotFound("Stock not found");
                }
                return Ok(find);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("CreateStock")]
        public async Task<IActionResult> AddStock([FromBody] CreateStockDTO stockDTO)
        {
            try
            {
                await stockRepository.AddStockAsync(stockDTO);
                return Ok("Stock added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("UpdateStock/{Id:int}")]
        public async Task<IActionResult> UpdateStock(int Id, [FromBody] CreateStockDTO stockDTO)
        {
            try
            {
                await stockRepository.UpdateStockAsync(Id, stockDTO);
                return Ok("Stock updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("DeleteStock/{Id:int}")]
        public async Task<IActionResult> DeleteStock(int Id)
        {
            try
            {
                await stockRepository.DeleteStockAsync(Id);
                return Ok("Stock deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
