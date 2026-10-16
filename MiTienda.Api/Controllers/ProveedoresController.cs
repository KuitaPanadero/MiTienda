using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTienda.Api.Data;
using MiTienda.Api.Models;

namespace MiTienda.Api.Controllers
{
    [ApiController]
    [Route("api/proveedores")]
    public class ProveedoresController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProveedoresController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Proveedor>>> Listar()
        {
            return await _db.Proveedores.OrderBy(p => p.Nombre).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> Buscar(int id)
        {
            var proveedor = await _db.Proveedores.FindAsync(id);
            if (proveedor == null) return NotFound();
            return proveedor;
        }

        [HttpPost]
        public async Task<ActionResult<Proveedor>> Crear(Proveedor proveedor)
        {
            _db.Proveedores.Add(proveedor);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = proveedor.IdProveedor }, proveedor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, Proveedor proveedor)
        {
            if (id != proveedor.IdProveedor) return BadRequest();

            var existe = await _db.Proveedores.AnyAsync(p => p.IdProveedor == id);
            if (!existe) return NotFound();

            _db.Entry(proveedor).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int id)
        {
            var proveedor = await _db.Proveedores.FindAsync(id);
            if (proveedor == null) return NotFound();

            _db.Proveedores.Remove(proveedor);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
