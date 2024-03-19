using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PieceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PieceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PieceModel>>> GetPiece()
        {
            var lista = await _context.pieces.ToListAsync();
            return Ok(lista);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<PieceModel>>> GetPieceById(int id)
        {
            var miobjeto = await _context.pieces.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<PieceModel>> CreatePiece(PieceModel objeto)
        {
            _context.pieces.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbPiece());
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<List<PieceModel>>> UpdatePieceStatus(PieceModel objeto)
        {
            var DbObjeto = await _context.pieces.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.piece_status = objeto.piece_status;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.pieces.ToListAsync());
        }

        [HttpPut("{id}/comments")]
        public async Task<ActionResult<List<PieceModel>>> UpdatePieceComments(PieceModel objeto)
        {
            var DbObjeto = await _context.pieces.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.comments = objeto.comments;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.pieces.ToListAsync());
        }

        [HttpPut("{id}/startdate")]
        public async Task<ActionResult<List<PieceModel>>> UpdatePieceStartDate(PieceModel objeto)
        {
            var DbObjeto = await _context.pieces.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.start_date = objeto.start_date;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.pieces.ToListAsync());
        }

        [HttpPut("{id}/enddate")]
        public async Task<ActionResult<List<PieceModel>>> UpdatePieceEndDate(PieceModel objeto)
        {
            var DbObjeto = await _context.pieces.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.end_date = objeto.end_date;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.pieces.ToListAsync());
        }

        [HttpPut("{id}/starttime")]
        public async Task<ActionResult<List<PieceModel>>> UpdatePieceStartTime(PieceModel objeto)
        {
            var DbObjeto = await _context.pieces.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.start_time = objeto.start_time;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.pieces.ToListAsync());
        }

        [HttpPut("{id}/endtime")]
        public async Task<ActionResult<List<PieceModel>>> UpdatePieceEndTime(PieceModel objeto)
        {
            var DbObjeto = await _context.pieces.FindAsync(objeto.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.end_time = objeto.end_time;
            objeto.updated_at = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(await _context.pieces.ToListAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<List<PieceModel>>> DeletePiece(int id)
        {
            var DbObjeto = await _context.pieces.FirstOrDefaultAsync(Ob => Ob.id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }
            _context.pieces.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbPiece());
        }

        private async Task<List<PieceModel>> GetDbPiece()
        {
            return await _context.pieces.ToListAsync();
        }
    }
}
