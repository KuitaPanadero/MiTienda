using System.ComponentModel.DataAnnotations;

namespace MiTienda.Api.Models
{
    public class MetodoPago
    {
        [Key]
        public int IdMetodoPago { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;
    }
}
