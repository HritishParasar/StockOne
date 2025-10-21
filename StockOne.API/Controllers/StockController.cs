using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockOne.API.Data;
using StockOne.API.Mapper;
using StockOne.API.Model.DTOs;

namespace StockOne.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllStocks")]
        public async Task<IActionResult> GetAllStocks()
        {
            try
            {
                var stocks = _context.Stocks.ToList().Select(x => x.MapToDto());
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

        }
        [HttpGet("GetStockById/{id}")]
        public async Task<IActionResult> GetStocksByID(int id)
        {
            try
            {
                var find = await _context.Stocks.FindAsync(id);
                if (find is null)
                {
                    return NotFound("Stock not found");
                }
                return Ok(find.MapToDto());
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
                var stockmModel = stockDTO.CreateDTO();
                _context.Stocks.Add(stockmModel);
                await _context.SaveChangesAsync();
                return Ok("Stock added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut("UpdateStock/{Id}")]
        public async Task<IActionResult> UpdateStock(int Id, [FromBody] CreateStockDTO stockDTO)
        {
            try
            {
                var existingStock = _context.Stocks.Find(Id);
                if (existingStock is null)
                {
                    return NotFound("Stock not found");
                }
                existingStock.CompanyName = stockDTO.CompanyName;
                existingStock.Symbol = stockDTO.Symbol;
                existingStock.Purchase = stockDTO.Purchase;
                existingStock.LastDiv = stockDTO.LastDiv;
                existingStock.Industry = stockDTO.Industry;
                existingStock.MarketCap = stockDTO.MarketCap;

                _context.Stocks.Update(existingStock);
                await _context.SaveChangesAsync();
                return Ok("Stock updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("DeleteStock/{Id}")]
        public async Task<IActionResult> DeleteStock(int Id)
        {
            try
            {
                var existingStock = _context.Stocks.Find(Id);
                if (existingStock is null)
                {
                    return NotFound("Stock not found");
                }
                _context.Stocks.Remove(existingStock);
                await _context.SaveChangesAsync();
                return Ok("Stock deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
