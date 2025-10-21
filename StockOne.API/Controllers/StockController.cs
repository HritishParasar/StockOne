using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockOne.API.Data;
using StockOne.API.Mapper;

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
                var stocks = _context.Stocks.ToList().Select( x => x.MapToDto());
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
                var find = _context.Stocks.Find(id);
                if(find is null)
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
    }
}
