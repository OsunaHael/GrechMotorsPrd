using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnitModel>>> GetUnit()
        {
            var lista = await _context.units.ToListAsync();
            return Ok(lista);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<UnitModel>>> GetSingleUnit(int id)
        {
            var miobjeto = await _context.units.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<UnitModel>> CreateModel(UnitModel objeto)
        {
            _context.units.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbModel());
        }

        [HttpPut("{id}/gnumber")]
        public async Task<ActionResult<List<UnitModel>>> UpdateModelGNumber(UnitModel objeto)
        {
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.g_number = objeto.g_number;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        [HttpPut("{id}/userid")]
        public async Task<ActionResult<List<UnitModel>>> UpdateModelUserId(UnitModel objeto)
        {
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.user_id = objeto.user_id;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        [HttpPut("{id}/color")]
        public async Task<ActionResult<List<UnitModel>>> UpdateModelColor(UnitModel objeto)
        {
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.color = objeto.color;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        [HttpPut("{id}/ext")]
        public async Task<ActionResult<List<UnitModel>>> UpdateModelExtended(UnitModel objeto)
        {
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.ext = objeto.ext;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        [HttpPut("{id}/model")]
        public async Task<ActionResult<List<UnitModel>>> UpdateModel(UnitModel objeto)
        {
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.model = objeto.model;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        [HttpPut("{id}/startdate")]
        public async Task<ActionResult<List<UnitModel>>> UpdateModelStartDate(UnitModel objeto)
        {
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.start_date = objeto.start_date;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        [HttpPut("{id}/enddate")]
        public async Task<ActionResult<List<UnitModel>>> UpdateModelEndDate(UnitModel objeto)
        {
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.end_date = objeto.end_date;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<UnitModel>>> DeleteModel(int id)
        {
            var DbObjeto = await _context.units.FirstOrDefaultAsync(Ob => Ob.id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }
            _context.units.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbModel());
        }


        private async Task<List<UnitModel>> GetDbModel()
        {
            return await _context.units.ToListAsync();
        }
    }
}
