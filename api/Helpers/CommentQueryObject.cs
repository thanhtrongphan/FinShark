using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    // Query object for comment to filter by symbol and sort by date
    public class CommentQueryObject
    {
        public string? symbol { get; set; } = null;
        public bool isDescending { get; set; } = false;
    }
}