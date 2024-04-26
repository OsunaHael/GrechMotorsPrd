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
    public class UnitController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Unit
        [HttpGet]
        public async Task<ActionResult<List<UnitModel>>> GetUnit()
        {
            // Retrieve all units from the database
            var lista = await _context.units.ToListAsync();
            return Ok(lista);
        }

        // GET: api/Unit/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<UnitModel>>> GetSingleUnit(int id)
        {
            // Retrieve a single unit from the database based on the provided id
            var miobjeto = await _context.units.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        // GET: api/Unit/getGnumber/{gnumber}
        [HttpGet]
        [Route("getGnumber/{gnumber}")]
        public async Task<ActionResult<List<UnitModel>>> GetUnitByGnumber(int gnumber)
        {
            // Retrieve a single unit from the database based on the provided id
            var miobjeto = await _context.units.FirstOrDefaultAsync(ob => ob.g_number == gnumber);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        // GET: api/Unit/getModel/{model}
        [HttpGet]
        [Route("getModel/{model}")]
        public async Task<ActionResult<List<UnitModel>>> GetUnitByModel(string model)
        {
            // Retrieve all units from the database that match the provided model name
            var units = await _context.units.Where(ob => ob.model == model).ToListAsync();
            if (units == null || units.Count == 0)
            {
                return NotFound(" :/");
            }
            return Ok(units);
        }
        
        // GET: api/Unit/getUnitById

        // POST: api/Unit
        [HttpPost]
        public async Task<ActionResult<UnitModel>> CreateUnit(UnitModel objeto)
        {
            // Agrega la unidad al contexto y guarda los cambios en la base de datos
            _context.units.Add(objeto);
            await _context.SaveChangesAsync();

            // Después de guardar los cambios, el ID de la unidad se actualizará en el objeto 'objeto'
            // Puedes acceder al ID recién asignado desde el objeto 'objeto'
            int newUnitId = objeto.id;

            // Puedes retornar el objeto con el ID asignado y el estado de la operación
            return Ok(objeto);
        }

        // PUT: api/Unit/{id}/gnumber
        [HttpPut("{id}/update/gnumber")]
        public async Task<ActionResult<List<UnitModel>>> UpdateUnitGNumber(UnitModel objeto)
        {
            // Update the g_number property of a unit based on the provided id
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.g_number = objeto.g_number;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        // PUT: api/Unit/{id}/userid
        [HttpPut("{id}/userid")]
        public async Task<ActionResult<List<UnitModel>>> UpdateUnitUserId(UnitModel objeto)
        {
            // Update the user_id property of a unit based on the provided id
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.user_id = objeto.user_id;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        // PUT: api/Unit/{id}/color
        [HttpPut("{id}/color")]
        public async Task<ActionResult<List<UnitModel>>> UpdateUnitColor(UnitModel objeto)
        {
            // Update the color property of a unit based on the provided id
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.color = objeto.color;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        // PUT: api/Unit/{id}/ext
        [HttpPut("{id}/ext")]
        public async Task<ActionResult<List<UnitModel>>> UpdateUnitExtended(UnitModel objeto)
        {
            // Update the ext property of a unit based on the provided id
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.ext = objeto.ext;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        // PUT: api/Unit/{id}/unit
        [HttpPut("{id}/unit")]
        public async Task<ActionResult<List<UnitModel>>> UpdateUnit(UnitModel objeto)
        {
            // Update the model property of a unit based on the provided id
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.model = objeto.model;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        // PUT: api/Unit/{id}/startdate
        [HttpPut("{id}/startdate")]
        public async Task<ActionResult<List<UnitModel>>> UpdateUnitStartDate(UnitModel objeto)
        {
            // Update the start_date property of a unit based on the provided id
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.start_date = objeto.start_date;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        // PUT: api/Unit/{id}/enddate
        [HttpPut("{id}/enddate")]
        public async Task<ActionResult<List<UnitModel>>> UpdateUnitEndDate(UnitModel objeto)
        {
            // Update the end_date property of a unit based on the provided id
            var DbObjeto = await _context.units.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.end_date = objeto.end_date;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.units.ToListAsync());
        }

        // DELETE: api/Unit/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<UnitModel>>> DeleteUnit(int id)
        {
            // Delete a unit from the database based on the provided id
            var DbObjeto = await _context.units.FirstOrDefaultAsync(Ob => Ob.id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }
            _context.units.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbUnit());
        }

        // Helper method to retrieve all units from the database
        private async Task<List<UnitModel>> GetDbUnit()
        {
            return await _context.units.ToListAsync();
        }
    }
}
