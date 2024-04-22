using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureStatusHistoryController : ControllerBase
    { 
        private readonly ApplicationDbContext _context;

        public FurnitureStatusHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<FurnitureStatusHistory>>> GetFurnitureStauses()
        {
            var lista = await _context.furniturestatushistories.ToListAsync();
            return Ok(lista);
        }

        // GET: api/FurnitureStatusHistory/getFurnitureStatusHistoryById/{id}
        [HttpGet]
        [Route("getFurnitureStatusHistoryById/{id}")]
        public async Task<ActionResult<List<FurnitureStatusHistory>>> GetFurnitureStatusHistoryById(int id)
        {
            var miobjeto = await _context.furniturestatushistories.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpGet]
        [Route("getFurnitureStatusHistoryByFurnitureId/{id}")]
        public async Task<ActionResult<List<FurnitureStatusHistory>>> GetFurnitureStatusHistoryByFurnitureId(int id)
        {
            var miobjeto = await _context.furniturestatushistories.FirstOrDefaultAsync(ob => ob.furniture_id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<FurnitureStatusHistory>> CreateFurnitureStatusHistory(FurnitureStatusHistory objeto)
        {

            _context.furniturestatushistories.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbFurnitureStatusHistory());
        }

        [HttpPut]
        public async Task<ActionResult<FurnitureStatusHistory>> UpdateFurnitureStatusHistory(FurnitureStatusHistory objeto)
        {
            _context.furniturestatushistories.Update(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbFurnitureStatusHistory());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FurnitureStatusHistory>> DeleteFurnitureStatusHistory(int id)
        {
            var objeto = await _context.furniturestatushistories.FindAsync(id);
            if (objeto == null)
            {
                return NotFound(" :/");
            }
            _context.furniturestatushistories.Remove(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbFurnitureStatusHistory());
        }

        private async Task<List<FurnitureStatusHistory>> GetDbFurnitureStatusHistory()
        {
            return await _context.furniturestatushistories.ToListAsync();
        }
    }
}