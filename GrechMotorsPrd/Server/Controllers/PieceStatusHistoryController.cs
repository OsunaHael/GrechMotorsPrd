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
    public class PieceStatusHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PieceStatusHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PieceStatusHistory>>> GetPieces()
        {
            var lista = await _context.piecestatushistories.ToListAsync();
            return Ok(lista);
        }

        // GET: api/PieceStatusHistory/getPieceStatusHistoryById/{id}
        [HttpGet]
        [Route("getPieceStatusHistoryById/{id}")]
        public async Task<ActionResult<List<PieceStatusHistory>>> GetPieceStatusHistoryById(int id)
        {
            var miobjeto = await _context.piecestatushistories.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpGet]
        [Route("getPieceStatusHistoryByPieceId/{id}")]
        public async Task<ActionResult<List<PieceStatusHistory>>> GetPieceStatusHistoryByPieceId(int id)
        {
            var miobjeto = await _context.piecestatushistories.FirstOrDefaultAsync(ob => ob.piece_id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        [HttpPost]
        public async Task<ActionResult<PieceStatusHistory>> CreatePieceStatusHistory(PieceStatusHistory objeto)
        {

            _context.piecestatushistories.Add(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbPieceStatusHistory());
        }

        [HttpPut]
        public async Task<ActionResult<PieceStatusHistory>> UpdatePieceStatusHistory(PieceStatusHistory objeto)
        {
            _context.piecestatushistories.Update(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbPieceStatusHistory());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PieceStatusHistory>> DeletePieceStatusHistory(int id)
        {
            var objeto = await _context.piecestatushistories.FindAsync(id);
            if (objeto == null)
            {
                return NotFound(" :/");
            }
            _context.piecestatushistories.Remove(objeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbPieceStatusHistory());
        }

        private async Task<List<PieceStatusHistory>> GetDbPieceStatusHistory()
        {
            return await _context.piecestatushistories.ToListAsync();
        }
    }
}
