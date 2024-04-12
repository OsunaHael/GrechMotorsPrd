using GrechMotorsPrd.Server.Data;
using GrechMotorsPrd.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrechMotorsPrd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitFurnitureCodeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UnitFurnitureCodeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UnitFurnitureCode
        [HttpGet]
        public async Task<ActionResult<List<UnitFurnitureCodeModel>>> GetUnitFurnitureCode()
        {
            // Retrieve all units from the database
            var lista = await _context.unitsfurniturescodes.ToListAsync();
            return Ok(lista);
        }

        // GET: api/UnitFurnitureCode/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<UnitFurnitureCodeModel>>> GetSingleUnitFurnitureCode(int id)
        {
            // Retrieve a single unit from the database based on the provided id
            var miobjeto = await _context.unitsfurniturescodes.FirstOrDefaultAsync(ob => ob.id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }
            return Ok(miobjeto);
        }

        //Get: api/UnitFurnitureCode/getFurnituresQrIdentificationCodeByUnit/{unit_id}
        [HttpGet]
        [Route("getFurnituresQrIdentificationCodesByUnit/{unit_id}")]
        public async Task<ActionResult<List<string>>> GetFurnitureQrIdentificationCodesByUnit(int unit_id)
        {
            // Retrieve all qr_code_numbers from the units in the database that match the provided unit_id
            var qrCodeNumbers = await _context.unitsfurniturescodes
                .Where(ob => ob.unit_id == unit_id)
                .Select(ob => ob.qr_code_number)
                .ToListAsync();

            if (qrCodeNumbers == null || qrCodeNumbers.Count == 0)
            {
                return NotFound(" :/");
            }

            return Ok(qrCodeNumbers);
        }

        [HttpPost]
        public async Task<ActionResult<UnitFurnitureCodeModel>> PostUnitFurnitureCode(UnitFurnitureCodeModel unitFurnitureCodeModel)
        {
            // Agrega la unidad al contexto y guarda los cambios en la base de datos
            _context.unitsfurniturescodes.Add(unitFurnitureCodeModel);
            await _context.SaveChangesAsync();
            return Ok(unitFurnitureCodeModel);
        }

        [HttpPut]
        public async Task<ActionResult<UnitFurnitureCodeModel>> PutUnitFurnitureCode(UnitFurnitureCodeModel unitFurnitureCodeModel)
        {
            // Update the g_number property of a unit based on the provided id
            var DbObjeto = await _context.unitsfurniturescodes.FindAsync(unitFurnitureCodeModel.id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            DbObjeto.qr_code_number = unitFurnitureCodeModel.qr_code_number;
            await _context.SaveChangesAsync();
            return Ok(await _context.unitsfurniturescodes.ToListAsync());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<UnitFurnitureCodeModel>> DeleteUnitFurnitureCode(int id)
        {
            // Delete a unit from the database based on the provided id
            var DbObjeto = await _context.unitsfurniturescodes.FirstOrDefaultAsync(Ob => Ob.id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }
            _context.unitsfurniturescodes.Remove(DbObjeto);
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