using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurniturePieceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FurniturePieceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<FurniturePieceModel>>> GetFurniturePiece()
        {
            var lista = await _context.furniturespieces.ToListAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<FurniturePieceModel>>> GetSingleFurniturePiece(int id)
        {
            var miobjeto = await _context.furniturespieces.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<FurniturePieceModel>> CreateFurniturePiece(FurniturePieceModel objeto)
        {
            _context.furniturespieces.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbFurniturePiece());
        }

        [HttpDelete]
        public async Task<ActionResult<List<FurniturePieceModel>>> DeleteFurniturePiece(FurniturePieceModel objeto)
        {
            var DbObjeto = await _context.furniturespieces.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            _context.furniturespieces.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await _context.furniturespieces.ToListAsync());
        }

        private async Task<List<FurniturePieceModel>> GetDbFurniturePiece()
        {
            return await _context.furniturespieces.ToListAsync();
        }
    }
}