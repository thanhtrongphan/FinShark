using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfraces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
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
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Stock>> GetStocks()
        {
            return await _context.Stocks.ToListAsync();
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