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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet]
        [Route("getPieceByFurniture/{furniture_id}")]
        public async Task<ActionResult<List<FurniturePieceModel>>> GetPieceByFurniture(int furniture_id)
        {
            var miobjeto = await _context.furniturespieces.FirstOrDefaultAsync(ob => ob.id == furniture_id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }
        
        [HttpGet]
        [Route("getPieceIdByFurniture/{furniture_id}")]
        public async Task<ActionResult<int>> GetPieceIdByFurniture(int furniture_id)
        {
            var miobjeto = await _context.furniturespieces.FirstOrDefaultAsync(ob => ob.id == furniture_id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto.piece_id);
        }

        // GET: api/FurniturePiece/getFurnitureByPieceId/{piece_id}
        [HttpGet]
        [Route("getFurnitureByPieceId/{piece_id}")]
        public async Task<ActionResult<List<int>>> GetFurnitureByPieceId(int piece_id)
        {
            var furniturePieces = await _context.furniturespieces
                .Where(ob => ob.piece_id == piece_id)
                .Select(ob => ob.furniture_id)
                .ToListAsync();

            if (furniturePieces.Count == 0)
            {
                return NotFound(" :/");
            }

            return Ok(furniturePieces);
        }

        // GET: api/FurniturePiece/getPiecesByFurnitureId/{id}
        [HttpGet]
        [Route("getPiecesByFurnitureId/{furniture_id}")]
        public async Task<ActionResult<List<int>>> GetPiecesByFurnitureId(int furniture_id)
        {
            var furniturePieces = await _context.furniturespieces
                .Where(ob => ob.furniture_id == furniture_id)
                .Select(ob => ob.piece_id)
                .ToListAsync();

            if (furniturePieces.Count == 0)
            {
                return NotFound(" :/");
            }

            return Ok(furniturePieces);
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