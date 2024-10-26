using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    // Represents a comment entity with properties such as Id, Title, Content, CreatedOn, StockID, and Stock.
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockID { get; set; }
        public Stock? Stock { get; set; }
    }
}
