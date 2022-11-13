using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace eGym.Models
{
    public class Ropa
    {
        [Key]
        public int idRopa { get; set; }
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [Display(Name = "Descripción")]
        public string detalles { get; set; }
        [Display(Name = "Precio")]
        public double precio { get; set; }
        [Display(Name = "Imagen")]
        public string ? imagenRopa { get; set; }
         
        //relaciones
        public List<Ropa_Color> ? ropas_colores { get; set; }

        //relacion-marca
        [Display(Name = "Marca")]
        public int marcaId { get; set; }
        [ForeignKey("marcaId")]
        public Marca ? Marca { get; set; }

        //relacion-tienda
        [Display(Name = "Tienda")]
        public int tiendaId { get; set; }
        [ForeignKey("tiendaId")]
        public Tienda ? Tienda { get; set; }

        //relacion-categoria
        [Display(Name = "Categoría")]
        public int categoriaId { get; set; }
        [ForeignKey("categoriaId")]
        public Categoria? Categoria { get; set; }

    }
}
