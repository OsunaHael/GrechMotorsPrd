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

        [HttpGet]
        [Route("getFurnitureByUnit/{unit_id}")]
        public async Task<ActionResult<List<UnitFurnitureModel>>> GetFurnitureByUnit(int unit_id)
        {
            var miobjeto = await _context.unitsfurnitures.FirstOrDefaultAsync(ob => ob.id == unit_id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }


        //public async Task<ActionResult<List<UnitFurnitureModel>>> GetFurnituresByUnit(int unit_id)
        //{
        //    // Retrieve all units from the database that match the provided model name
        //    var unitFurnitures = await _context.unitsfurnitures.Where(ob => ob.unit_id == unit_id).ToListAsync();
        //    if (unitFurnitures == null || unitFurnitures.Count == 0)
        //    {
        //        return NotFound(" :/");
        //    }
        //    return Ok(unitFurnitures);
        //}
        // GET: api/UnitFurniture/getFurnituresByUnit/{unit_id}
        [HttpGet]
        [Route("getFurnituresByUnit/{unit_id}")]
        public async Task<ActionResult<List<int>>> GetFurnituresByUnit(int unit_id)
        {
            var furnitureIds = await _context.unitsfurnitures
                .Where(ob => ob.unit_id == unit_id)
                .Select(ob => ob.furniture_id)
                .ToListAsync();

            if (furnitureIds == null || furnitureIds.Count == 0)
            {
                return Ok(new List<int>());
            }

            return Ok(furnitureIds);
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