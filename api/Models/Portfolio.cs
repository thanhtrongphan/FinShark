using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    // sub table connect between user and stock
    // 1 portfolio belong to 1 user and 1 stock
    [Table("portfolios")]
    public class Portfolio
    {
        public string AppUserID { get; set; }
        public int StockID { get; set; }
        public AppUser AppUser { get; set; }
        public Stock Stock { get; set; }

    }
}