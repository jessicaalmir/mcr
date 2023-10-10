using System.Security.Cryptography;
using System.Text;
using mcr.Data.Models;
using mcr.Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mcr.Business.IServices;
using mcr.Data;
using Azure.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace mcr.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper){
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] UserRegistrationDto model){
           
            if( await UserExists(model.UserName))
                return Conflict("User already created");

            var newUser = _mapper.Map<AppUser>(model);
            
            newUser.UserName = model.UserName.ToLower();
            
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if(!result.Succeeded)
                return BadRequest(result.Errors);
            return new UserDto{
                UserName = newUser.UserName,
                Token = _tokenService.CreateToken(newUser)
            };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] UserLoginDto model){
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == model.UserName);

            if(user == null)
                return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if(!result)
                return Unauthorized();

            return new UserDto{
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string userName){
            return await _userManager.Users.AnyAsync(x=>x.UserName == userName.ToLower());
        }

    }

}