using System.Security.Cryptography;
using System.Text;
using mcr.Data.Models;
using mcr.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mcr.Business.IServices;
using mcr.Data;
using Azure.Identity;

namespace mcr.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserRegistrationDto model){
           
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

            return new UserDto{
                UserName = newUser.UserName,
                Token = _tokenService.CreateToken(newUser)
            };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AppUser>> Login([FromBody] UserLoginDto model){
            var user = await _context.AppUsers.SingleOrDefaultAsync(x => x.UserName == model.UserName);

            if(user == null)
                return Unauthorized();

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

            for(int i = 0; i< computedHash.Length; i++){
                if(computedHash[i] !=user.PasswordHash[i])
                    return Unauthorized("Invalid Password");
            }

            return user;
        }

        private async Task<bool> UserExists(string userName){
            return await _context.AppUsers.AnyAsync(x=>x.UserName == userName.ToLower());
        }

    }

}