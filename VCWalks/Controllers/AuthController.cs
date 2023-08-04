using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using VCWalks.Models.DTO;
using VCWalks.Repository;

namespace VCWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //Post/Api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Password
            };

            var identityResult =await userManager.CreateAsync(identityUser, registerRequestDTO.Password);
            if(identityResult.Succeeded)
            {
                //Add roles to this user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }

            }

            return BadRequest("Something went wrong");
        }

        //Post: api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Username);
           
            if (user != null)
            {
                var checkPasswordResult =await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
               
                if(checkPasswordResult)
                {
                    //Get roles for thwe User
                    var roles = await userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        //Creating a Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponceDTO
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(jwtToken);
                    }
                }
            }
            return BadRequest("UserName or password incorrect");
        }
    }
}
