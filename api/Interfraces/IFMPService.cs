using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfraces
{
    // interface for FMPService
    public interface IFMPService
    {
        Task<Stock> FindStockBySymbol(string symbol);
    }
}