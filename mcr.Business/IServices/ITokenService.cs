using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mcr.Data.Models;

namespace mcr.Business.IServices
{
    public interface ITokenService
    {
       public string CreateToken(AppUser appUser);
    }
}