using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfraces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await _stockRepository.GetStocks();
            var stockDtos = stocks.Select(x => x.ToStockDto()).ToList();
            return Ok(stockDtos);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockID([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.GetStock(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }
        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequest stockDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockToCreateDTO();
            await _stockRepository.AddStock(stockModel);
            return CreatedAtAction(nameof(GetStockID), new {id = stockModel.Id}, stockModel.ToStockDto());

        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute]int id, [FromBody] UpdateStockRequest stockDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.GetStock(id);
            if (stock == null)
            {
                return NotFound();
            }
            var stockModel = stockDto.ToStockToUpdateDTO();
            await _stockRepository.UpdateStock(id, stockModel);
            return Ok(stock.ToStockDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            await _stockRepository.DeleteStock(id);
            return NoContent();
        }
    }
}