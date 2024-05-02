using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GrechMotorsPrd.Shared.DTOs;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ApplicationDbContext _context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this._context = _context;
        }

        [HttpGet]
        [Route("getLastUserId")]
        public async Task<ActionResult<UserDto>> GetLastUserId()
        {
            var lastUser = await userManager.Users.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
            if (lastUser == null)
            {
                return NotFound("No users found.");
            }
            return Ok(new UserDto { Id = lastUser.Id, PasswordHash = lastUser.PasswordHash });
        }

        [HttpGet("renovateToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserToken>> RenovateToken()
        {
            var userInfo = new UserModel
            {
                username = HttpContext.User.Identity!.Name!
            };
            return await BuildToken(userInfo);
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserModel userModel)
        {
            var user = new ApplicationUser { UserName = userModel.username, Email = userModel.email };
            var result = await userManager.CreateAsync(user, userModel.pwd!);
            if (result.Succeeded)
            {
                return await BuildToken(userModel); // Pasar el objeto userModel a BuildToken
            }
            else
            {
                return BadRequest(result.Errors.First());
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserModel userModel)
        {
            var user = await userManager.FindByEmailAsync(userModel.email!);
            var result = await signInManager.PasswordSignInAsync(user.UserName!, userModel.pwd!, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return await BuildToken(userModel);
            }
            else
            {
                return BadRequest("Invalid login attempt");
            }
        }

        private async Task<UserToken> BuildToken(UserModel userModel)
        {
            var user = await userManager.FindByEmailAsync(userModel.email!);
            var roles = await userManager.GetRolesAsync(user!);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!)
            };
            
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

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
