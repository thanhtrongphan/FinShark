using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfraces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    // manage stock controller
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock?> AddStock(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> GetStock(int id)
        {
            return await _context.Stocks.Include(c => c.Comments)
            .ThenInclude(c => c.AppUser)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Stock?> GetStockBySymbol(string symbol)
        {
            return _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }

        public async Task<List<Stock>> GetStocks(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).ThenInclude(c => c.AppUser).AsQueryable();
            if (!string.IsNullOrEmpty(query.Symbol))
            {
                stocks = stocks.Where(x => x.Symbol == query.Symbol);
            }
            if (!string.IsNullOrEmpty(query.CompanyName))
            {
                stocks = stocks.Where(x => x.CompanyName == query.CompanyName);
            }
            if(!string.IsNullOrEmpty(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(x => x.Symbol) : stocks.OrderBy(x => x.Symbol);
                }
            }
            var skip = (query.Page - 1) * query.PageSize;
            stocks = stocks.Skip(skip).Take(query.PageSize);
            return await stocks.ToListAsync();
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateStock(int id, Stock stockModel)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            stock.Symbol = stockModel.Symbol;
            stock.CompanyName = stockModel.CompanyName;
            stock.Industry = stockModel.Industry;
            stock.Purchase = stockModel.Purchase;
            stock.LastDiv = stockModel.LastDiv;
            stock.MarketCap = stockModel.MarketCap;
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}