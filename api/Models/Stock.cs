using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    // Create table stocks
    // String.empty is used to ensure that the properties are not null when a Stock object is created
    // Column attribute is used to specify the data type and size for table columns
    [Table("stocks")]
    public class Stock
    {
        public int Id { get; set; }
        
        // Initializes the Symbol property with an empty string to ensure it's not null when a Stock object is created
        public string Symbol { get; set; } = String.Empty; 
        public string CompanyName { get; set; } = String.Empty;

        // Set type column when creating the table in the database
        [Column(TypeName = "decimal(18, 2)")] 
        public decimal Purchase { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastDiv { get; set; }
        
        public string Industry { get; set; } = String.Empty; 
        
        public long MarketCap { get; set; }
        
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
