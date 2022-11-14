using System.ComponentModel.DataAnnotations;

namespace eGym.Models
{
    public class Categoria
    {
        [Key]
        public int idCategoria { get; set; }
        [Display(Name ="Categoría")]
        public string nombre { get; set; }

        //relaciones
        public List<Ropa> Ropas { get; set; }
    }
}
