using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _context.Stocks.Select(stock => stock.ToStockDto()).ToList();
            return Ok(stocks);
        }
        [HttpGet("{id}")]
        public IActionResult GetStockID([FromRoute]int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequest stockDto)
        {
            var stockModel = stockDto.ToStockToCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStockID), new {id = stockModel.Id}, stockModel.ToStockDto());

        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStock([FromRoute]int id, [FromBody] UpdateStockRequest stockDto)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;
            _context.SaveChanges();
            return Ok(stock.ToStockDto());
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteStock([FromRoute]int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
            return NoContent();
        }
    }
}