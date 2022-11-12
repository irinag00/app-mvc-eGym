using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace eGym.Models
{
    public class Tienda
    {
        [Key]
        public int idTienda { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Página Web")]
        public string resumen { get; set; }
        [Display(Name = "Imagen")]
        public string ? imagenTienda { get; set; }

        //relaciones
        public List<Ropa> ? Ropas { get; set; }
    }
}
