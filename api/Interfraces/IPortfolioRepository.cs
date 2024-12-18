using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfraces
{
    // interface for PortfolioRepository
    public interface IPortfolioRepository
    {
        public Task<List<Stock>> GetUserPortfolios(AppUser user);
        public Task<Portfolio?> AddPortfolio(Portfolio portfolio);
        public Task<Portfolio?> DeletePortfolio(AppUser user, string symbol);
    }
}