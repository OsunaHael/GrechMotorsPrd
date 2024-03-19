using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFurnitureController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserFurnitureController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserFurnitureModel>>> GetUserFurniture()
        {
            var lista = await _context.usersfurnitures.ToListAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<UserFurnitureModel>>> GetSingleUserFurniture(int id)
        {
            var miobjeto = await _context.usersfurnitures.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<UserFurnitureModel>> CreateUserFurniture(UserFurnitureModel objeto)
        {
            _context.usersfurnitures.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbUserFurniture());
        }

        [HttpDelete]
        public async Task<ActionResult<List<UserFurnitureModel>>> DeleteUserFurniture(UserFurnitureModel objeto)
        {
            var DbObjeto = await _context.usersfurnitures.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            _context.usersfurnitures.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await _context.usersfurnitures.ToListAsync());
        }

        private async Task<List<UserFurnitureModel>> GetDbUserFurniture()
        {
            return await _context.usersfurnitures.ToListAsync();
        }
    }
}
