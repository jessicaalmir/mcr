using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mcr.Data.Models;

namespace mcr.Data.Authentication
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string JwtToken { get; set; }
        public DateTime Expiration { get; set; }

        /*public AuthenticateResponse(AppUser user, string token, DateTime expiration){
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            JwtToken = token;
            Expiration = expiration;
        }*/

    }
}