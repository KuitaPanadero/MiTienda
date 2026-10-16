using Microsoft.EntityFrameworkCore;
using MiTienda.Api.Models;

namespace MiTienda.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opciones)
            : base(opciones)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<MetodoPago> MetodosPago { get; set; }   // NUEVO
        public DbSet<Proveedor> Proveedores { get; set; }    // NUEVO

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Clientes de ejemplo para que la tienda no arranque vacia
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, Nombre = "Ana Gómez", Correo = "ana@correo.com", Telefono = "3001234567" },
                new Cliente { Id = 2, Nombre = "Luis Pérez", Correo = "luis@correo.com", Telefono = "3109876543" },
                new Cliente { Id = 3, Nombre = "Sara Ruiz", Correo = "sara@correo.com", Telefono = "3205551234" }
            );

            // NUEVO: metodos de pago de ejemplo
            modelBuilder.Entity<MetodoPago>().HasData(
                new MetodoPago { IdMetodoPago = 1, Nombre = "Efectivo" },
                new MetodoPago { IdMetodoPago = 2, Nombre = "Tarjeta de credito" },
                new MetodoPago { IdMetodoPago = 3, Nombre = "PSE" }
            );

            // NUEVO: proveedores de ejemplo
            modelBuilder.Entity<Proveedor>().HasData(
                new Proveedor { IdProveedor = 1, Nombre = "Distribuidora Andina", Telefono = "6041234567" },
                new Proveedor { IdProveedor = 2, Nombre = "Importaciones del Valle", Telefono = "6047654321" }
            );
        }
    }
}
