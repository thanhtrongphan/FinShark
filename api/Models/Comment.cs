using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    // Create table comments
    // int? Stock? is can be null
    [Table("comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        // 1 comment belong to 1 stock
        public int? StockID { get; set; }
        public Stock? Stock { get; set; }
        // 1 comment belong to 1 user
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
