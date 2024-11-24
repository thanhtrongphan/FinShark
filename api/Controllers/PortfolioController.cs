using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfraces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPortfolios()
        {
            var user = User.GetUserName(); 
            var appUser = await _userManager.FindByNameAsync(user);
            var stocks = await _portfolioRepository.GetUserPortfolios(appUser);
            return Ok(stocks);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var user = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(user);

            var stock = await _stockRepository.GetStockBySymbol(symbol);

            if (stock == null)
            {
                return NotFound("Stock not found");
            }

            var portfolio = new Portfolio
            {
                AppUserID = appUser.Id,
                StockID = stock.Id
            };

            var portfolioModel = await _portfolioRepository.AddPortfolio(portfolio);
            if (portfolioModel == null)
            {
                return BadRequest("Failed to add stock to portfolio");
            }
            else
            {
                return Created();
            }
        }
    }
}