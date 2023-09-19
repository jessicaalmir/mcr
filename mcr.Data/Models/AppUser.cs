using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace mcr.Data.Models
{
    public class AppUser: BaseEntity<int>{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string UserName{get; set;}
        [Required]
        public string Email{get; set;}
        public byte[]? PasswordHash{get; set;}
        public byte[]? PasswordSalt{get; set;}
    }
}