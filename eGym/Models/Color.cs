using System.ComponentModel.DataAnnotations;

namespace eGym.Models
{
    public class Color
    {
        [Key]
        public int idColor { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Imagen")]
        public string? imagenColor { get; set;}

        //relaciones
        public List<Ropa_Color> ? ropas_colores { get; set; }
    }
}
