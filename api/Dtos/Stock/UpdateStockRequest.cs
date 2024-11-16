using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockRequest
    {
        [Required]
        [MaxLength(5, ErrorMessage = "Symbol must be less than 5 characters")]
        public string Symbol { get; set; } = String.Empty; 
        [Required]
        [MinLength(3, ErrorMessage = "Company Name must be at least 3 characters")]
        [MaxLength(280, ErrorMessage = "Company Name must be less than 280 characters")]
        public string CompanyName { get; set; } = String.Empty;
        [Required]
        [Range(0, 1000000, ErrorMessage = "Purchase must be between 0 and 1000000")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100, ErrorMessage = "LastDiv must be between 0.001 and 100")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry must be less than 10 characters")]
        public string Industry { get; set; } = String.Empty; 
         [Required]
        [Range(0, 50000000, ErrorMessage = "MarketCap must be between 0 and 50000000")]
        public long MarketCap { get; set; }
    }
}