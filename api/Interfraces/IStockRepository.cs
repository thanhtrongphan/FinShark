using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Interfraces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetStocks();
        public Task<Stock?> GetStock(int id);
        public Task<Stock?> AddStock(Stock stockModel);
        public Task<Stock?> UpdateStock(int id, Stock stockModel);
        public Task<Stock?> DeleteStock(int id);
    }
}