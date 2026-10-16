using System.ComponentModel.DataAnnotations;

namespace MiTienda.Api.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Correo { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Telefono { get; set; }
    }
}
