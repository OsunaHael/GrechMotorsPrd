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

        // GET: api/Account/getLastUserId
        [HttpGet]
        [Route("getLastUserId")]
        public async Task<ActionResult<int>> GetLastUserId()
        {
            var lastUser = await userManager.Users.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
            if (lastUser == null)
            {
                return NotFound("No users found.");
            }
            return Ok(lastUser.Id);
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
            var result = await signInManager.PasswordSignInAsync(userModel.email!, userModel.pwd!, isPersistent: false, lockoutOnFailure: false);
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
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userModel.username!),
                new Claim("miValor", "Lo que yo quiera"),
            };

            var user = await userManager.FindByEmailAsync(userModel.email!);
            var roles = await userManager.GetRolesAsync(user!);
            
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
