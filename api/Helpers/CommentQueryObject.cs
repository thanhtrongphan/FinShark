using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class CommentQueryObject
    {
        public string? symbol { get; set; } = null;
        public bool isDescending { get; set; } = false;
    }
}