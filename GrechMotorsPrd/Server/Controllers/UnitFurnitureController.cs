using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitFurnitureController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitFurnitureController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnitFurnitureModel>>> GetUnitFurniture()
        {
            var lista = await _context.unitsfurnitures.ToListAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<UnitFurnitureModel>>> GetSingleUnitFurniture(int id)
        {
            var miobjeto = await _context.unitsfurnitures.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<UnitFurnitureModel>> CreateUnitFurniture(UnitFurnitureModel objeto)
        {
            _context.unitsfurnitures.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbUnitFurniture());
        }

        [HttpDelete]
        public async Task<ActionResult<List<UnitFurnitureModel>>> DeleteUnitFurniture(UnitFurnitureModel objeto)
        {
            var DbObjeto = await _context.unitsfurnitures.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            _context.unitsfurnitures.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await _context.unitsfurnitures.ToListAsync());
        }

        private async Task<List<UnitFurnitureModel>> GetDbUnitFurniture()
        {
            return await _context.unitsfurnitures.ToListAsync();
        }
    }
}