using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace mcr.Data.Models
{
    public class AppUser: IdentityUser<int>{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}