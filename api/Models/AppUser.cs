using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    // Model User in database extends from IdentityUser default
    
    public class AppUser : IdentityUser
    {
        // 1 user have many portfolios
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}