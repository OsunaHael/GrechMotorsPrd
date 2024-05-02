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
    public class FurnitureController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FurnitureController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<FurnitureModel>>> GetFurniture()
        {
            var lista = await _context.furnitures.ToListAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<FurnitureModel>>> GetSingleFurniture(int id)
        {
            var miobjeto = await _context.furnitures.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpGet]
        [Route("getFurnitureById/{id}")]
        public async Task<ActionResult<List<FurnitureModel>>> GetFurnitureById(int id)
        {
            var miobjeto = await _context.furnitures.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        // GET: api/Furniture/getFurnituresById/{unit_id}
        [HttpGet]
        [Route("getFurnituresById/{id}")]
        public async Task<ActionResult<List<FurnitureModel>>> GetFurnituresById(int id)
        {
            // Retrieve all units from the database that match the provided model name
            var furnitures = await _context.furnitures.Where(ob => ob.id == id).ToListAsync();
            if (furnitures == null || furnitures.Count == 0)
            {
                return NotFound(" :/");
            }
            return Ok(furnitures);
        }

        [HttpPost]
        public async Task<ActionResult<FurnitureModel>> CreateFurniture(FurnitureModel objeto)
        {
            _context.furnitures.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbFurniture());
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<List<FurnitureModel>>> UpdateFurnitureStatus(FurnitureModel objeto)
        {
            var DbObjeto = await _context.furnitures.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.furniture_status = objeto.furniture_status;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.furnitures.ToListAsync());
        }

        [HttpPut("{id}/qrIdentificationCode")]
        public async Task<ActionResult<List<FurnitureModel>>> UpdateFurnitureQrIdentificationCode(FurnitureModel objeto)
        {
            var DbObjeto = await _context.furnitures.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.furniture_status = objeto.furniture_status;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.furnitures.ToListAsync());
        }

        [HttpPut("{id}/comments")]
        public async Task<ActionResult<List<FurnitureModel>>> UpdateFurnitureComments(FurnitureModel objeto)
        {
            var DbObjeto = await _context.furnitures.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.comments = objeto.comments;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.furnitures.ToListAsync());
        }

        [HttpPut("{id}/startdate")]
        public async Task<ActionResult<List<FurnitureModel>>> UpdateFurnitureStartDate(FurnitureModel objeto)
        {
            var DbObjeto = await _context.furnitures.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.start_date = objeto.start_date;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.furnitures.ToListAsync());
        }

        [HttpPut("{id}/enddate")]
        public async Task<ActionResult<List<FurnitureModel>>> UpdateFurnitureEndDate(FurnitureModel objeto)
        {
            var DbObjeto = await _context.furnitures.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.end_date = objeto.end_date;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.furnitures.ToListAsync());
        }

        [HttpPut("{id}/starttime")]
        public async Task<ActionResult<List<FurnitureModel>>> UpdateFurnitureStartTime(FurnitureModel objeto)
        {
            var DbObjeto = await _context.furnitures.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.start_time = objeto.start_time;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.furnitures.ToListAsync());
        }

        [HttpPut("{id}/endtime")]
        public async Task<ActionResult<List<FurnitureModel>>> UpdateFurnitureEndTime(FurnitureModel objeto)
        {
            var DbObjeto = await _context.furnitures.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.end_time = objeto.end_time;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.furnitures.ToListAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<FurnitureModel>>> DeleteFurniture(int id)
        {
            var DbObjeto = await _context.furnitures.FirstOrDefaultAsync(Ob => Ob.id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }
            _context.furnitures.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbFurniture());
        }

        private async Task<List<FurnitureModel>> GetDbFurniture()
        {
            return await _context.furnitures.ToListAsync();
        }
    }
}
