using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPieceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserPieceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserPieceModel>>> GetUserPiece()
        {
            var lista = await _context.userspieces.ToListAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<UserPieceModel>>> GetSingleUserPiece(int id)
        {
            var miobjeto = await _context.userspieces.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<UserPieceModel>> CreateUserPiece(UserPieceModel objeto)
        {
            _context.userspieces.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbUserPiece());
        }

        [HttpDelete]
        public async Task<ActionResult<List<UserPieceModel>>> DeleteUserPiece(UserPieceModel objeto)
        {
            var DbObjeto = await _context.userspieces.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            _context.userspieces.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await _context.userspieces.ToListAsync());
        }

        private async Task<List<UserPieceModel>> GetDbUserPiece()
        {
            return await _context.userspieces.ToListAsync();
        }
    }
}
