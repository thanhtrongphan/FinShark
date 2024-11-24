using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfraces
{
    public interface IPortfolioRepository
    {
        public Task<List<Stock>> GetUserPortfolios(AppUser user);
        public Task<Portfolio?> AddPortfolio(Portfolio portfolio);
    }
}