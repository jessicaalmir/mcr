using System.Security.Cryptography;
using System.Text;
using mcr.Data;
using mcr.Data.Models;
using mcr.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mcr.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationDto model){
           
            using var hmac = new HMACSHA512();

            if( await UserExists(model.UserName))
                return Conflict("User already created");

            var newUser = new AppUser{
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName.ToLower(),
                Email = model.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password)),
                PasswordSalt = hmac.Key
            };

            _context.AppUsers.Add(newUser);
            var result = await _context.SaveChangesAsync();

            return Ok(result);
        }

        /*[HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest model){
            var user = await _userManager.FindByNameAsync(model.UserName);

            if(user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized();

            var authClaims = new List<Claim>{
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };


        }*/
        private async Task<bool> UserExists(string userName){
            return await _context.AppUsers.AnyAsync(x=>x.UserName == userName.ToLower());
        }

    }
}