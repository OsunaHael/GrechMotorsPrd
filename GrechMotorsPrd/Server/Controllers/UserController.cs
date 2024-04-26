using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {

            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetUser()
        {
            var lista = await _context.users.ToListAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<UserModel>>> GetSingleUser(int id)
        {
            var miobjeto = await _context.users.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }

            return Ok(miobjeto);
        }

        // GET: api/UnitPieceCode/getUnitPieceByIdentificationCode/{qr_code_number}
        [HttpGet]
        [Route("getUserByUserNumber/{userNumber}")]
        public async Task<ActionResult<List<UserModel>>> GetUserByUserNumber(string userNumber)
        {
            var miobjeto = await _context.users.FirstOrDefaultAsync(ob => ob.user_number == userNumber);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel objeto)
        {

            _context.users.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbUser());
        }

        [HttpPut("{id}/username")]
        public async Task<ActionResult<List<UserModel>>> UpdateUsername(UserModel objeto)
        {
            var DbObjeto = await _context.users.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.username = objeto.username;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.users.ToListAsync());
        }

        [HttpPut("{id}/usernumber")]
        public async Task<ActionResult<List<UserModel>>> UpdateUserNumber(UserModel objeto)
        {

            var DbObjeto = await _context.users.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.user_number = objeto.user_number;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.users.ToListAsync());
        }

        [HttpPut("{id}/email")]
        public async Task<ActionResult<List<UserModel>>> UpdateUserEmail(UserModel objeto)
        {
            var DbObjeto = await _context.users.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.email = objeto.email;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.users.ToListAsync());
        }

        [HttpPut("{id}/password")]
        public async Task<ActionResult<List<UserModel>>> UpdateUserPassword(UserModel objeto)
        {
            var DbObjeto = await _context.users.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.pwd = objeto.pwd;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.users.ToListAsync());
        }

        [HttpPut("{id}/phonenumber")]
        public async Task<ActionResult<List<UserModel>>> UpdateUserPhoneNumber(UserModel objeto)
        {
            var DbObjeto = await _context.users.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.phone = objeto.phone;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.users.ToListAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<UserModel>>> DeleteUser(int id)
        {
            var DbObjeto = await _context.users.FirstOrDefaultAsync(Ob => Ob.id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }
            _context.users.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbUser());
        }

        private async Task<List<UserModel>> GetDbUser()
        {
            return await _context.users.ToListAsync();
        }
    }
}