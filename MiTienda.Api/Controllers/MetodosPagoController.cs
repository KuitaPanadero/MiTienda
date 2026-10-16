using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTienda.Api.Data;
using MiTienda.Api.Models;

namespace MiTienda.Api.Controllers
{
    [ApiController]
    [Route("api/metodospago")]
    public class MetodosPagoController : ControllerBase
    {
        private readonly AppDbContext _db;

        public MetodosPagoController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<MetodoPago>>> Listar()
        {
            return await _db.MetodosPago.OrderBy(m => m.Nombre).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MetodoPago>> Buscar(int id)
        {
            var metodoPago = await _db.MetodosPago.FindAsync(id);
            if (metodoPago == null) return NotFound();
            return metodoPago;
        }

        [HttpPost]
        public async Task<ActionResult<MetodoPago>> Crear(MetodoPago metodoPago)
        {
            _db.MetodosPago.Add(metodoPago);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = metodoPago.IdMetodoPago }, metodoPago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, MetodoPago metodoPago)
        {
            if (id != metodoPago.IdMetodoPago) return BadRequest();

            var existe = await _db.MetodosPago.AnyAsync(m => m.IdMetodoPago == id);
            if (!existe) return NotFound();

            _db.Entry(metodoPago).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int id)
        {
            var metodoPago = await _db.MetodosPago.FindAsync(id);
            if (metodoPago == null) return NotFound();

            _db.MetodosPago.Remove(metodoPago);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
