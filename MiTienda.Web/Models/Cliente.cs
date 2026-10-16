using System.ComponentModel.DataAnnotations;

namespace MiTienda.Web.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Escribe el nombre.")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Escribe el correo.")]
        [EmailAddress(ErrorMessage = "El correo no es válido.")]
        [MaxLength(100)]
        public string Correo { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Telefono { get; set; }
    }
}
