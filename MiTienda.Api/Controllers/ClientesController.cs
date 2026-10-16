using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTienda.Api.Data;
using MiTienda.Api.Models;

namespace MiTienda.Api.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ClientesController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Listar()
        {
            return await _db.Clientes.OrderBy(c => c.Nombre).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Buscar(int id)
        {
            var cliente = await _db.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();
            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Crear(Cliente cliente)
        {
            _db.Clientes.Add(cliente);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Buscar), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest();

            var existe = await _db.Clientes.AnyAsync(c => c.Id == id);
            if (!existe) return NotFound();

            _db.Entry(cliente).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int id)
        {
            var cliente = await _db.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            _db.Clientes.Remove(cliente);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
