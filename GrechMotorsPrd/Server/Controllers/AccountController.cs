using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserModel userModel)
        {
            var user = new IdentityUser { UserName = userModel.email, Email = userModel.email };
            var result = await userManager.CreateAsync(user, userModel.pwd);
            if (result.Succeeded)
            {
                return BuildToken(userModel);
            }
            else
            {
                return BadRequest(result.Errors.First());
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserModel userModel)
        {
            var result = await signInManager.PasswordSignInAsync(userModel.email, userModel.pwd, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return BuildToken(userModel);
            }
            else
            {
                return BadRequest("Invalid login attempt");
            }
        }

        private UserToken BuildToken(UserModel userModel)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userModel.email),
                new Claim("miValor", "Lo que yo quiera"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtkey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(1);
            var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
