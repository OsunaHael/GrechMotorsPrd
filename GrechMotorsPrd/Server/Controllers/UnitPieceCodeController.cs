using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitPieceCodeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitPieceCodeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UnitPieceCode
        [HttpGet]
        public async Task<ActionResult<List<UnitPieceCodeModel>>> GetUnitPieceCode()
        {
            var lista = await _context.unitspiecescodes.ToListAsync();
            return Ok(lista);
        }

        // GET: api/UnitPieceCode/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<UnitPieceCodeModel>>> GetSingleUnitPieceCode(int id)
        {
            var miobjeto = await _context.unitspiecescodes.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        // GET: api/UnitPieceCode/getUnitPieceByIdentificationCode/{qr_code_number}
        [HttpGet]
        [Route("getUnitPieceByIdentificationCode/{qr_code_number}")]
        public async Task<ActionResult<List<UnitPieceCodeModel>>> GetUnitPieceByIdentificationCode(string qr_code_number)
        {
            var miobjeto = await _context.unitspiecescodes.FirstOrDefaultAsync(ob => ob.qr_code_number == qr_code_number);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);     
        }

        [HttpPost]
        public async Task<ActionResult<UnitPieceCodeModel>> PostUnitPieceCode(UnitPieceCodeModel UnitPieceCodeModel)
        {
            _context.unitspiecescodes.Add(UnitPieceCodeModel);
            await _context.SaveChangesAsync();
            return Ok(UnitPieceCodeModel);
        }

        [HttpPut]
        public async Task<ActionResult<UnitPieceCodeModel>> PutUnitPieceCode(UnitPieceCodeModel UnitPieceCodeModel)
        {
            var DbObjeto = await _context.unitspiecescodes.FindAsync(UnitPieceCodeModel.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.qr_code_number = UnitPieceCodeModel.qr_code_number;
            await _context.SaveChangesAsync();
            return Ok(await _context.unitspiecescodes.ToListAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<UnitPieceCodeModel>> DeleteUnitPieceCode(int id)
        {
            var DbObjeto = await _context.unitspiecescodes.FirstOrDefaultAsync(Ob => Ob.id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }
            _context.unitspiecescodes.Remove(DbObjeto);
            await _context.SaveChangesAsync();
            return Ok(await GetDbUnit());
        }
        private async Task<List<UnitModel>> GetDbUnit()
        {
            return await _context.units.ToListAsync();
        }
    }
}