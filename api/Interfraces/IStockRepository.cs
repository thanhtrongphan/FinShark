using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfraces
{
    // Interface for Stock Repository
    public interface IStockRepository
    {
        public Task<List<Stock>> GetStocks(QueryObject query);
        public Task<Stock?> GetStock(int id);
        public Task<Stock?> AddStock(Stock stockModel);
        public Task<Stock?> UpdateStock(int id, Stock stockModel);
        public Task<Stock?> DeleteStock(int id);
        public Task<bool> StockExists(int id);
    }
}