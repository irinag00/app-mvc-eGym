using System.ComponentModel.DataAnnotations;

namespace eGym.Models
{
    public class Marca
    {
        [Key]
        public int idMarca { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Resumen")]
        public string resumen { get; set; }
        [Display(Name = "Imagen")]
        public string ? imagenMarca { get; set; }

        //relaciones
        public List<Ropa> ? Ropas { get; set; }
    }
}
