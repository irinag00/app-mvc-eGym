using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace eGym.Models
{
    public class Ropa
    {
        [Key]
        public int idRopa { get; set; }
        public string nombre { get; set; }
        public string detalles { get; set; }
        public double precio { get; set; }
        public string imagenRopa { get; set; }
         
        //relaciones
        public List<Ropa_Color> ropas_colores { get; set; }

        //relacion-marca
        public int marcaId { get; set; }
        [ForeignKey("marcaId")]
        public Marca Marca { get; set; }

        //relacion-tienda
        public int tiendaId { get; set; }
        [ForeignKey("tiendaId")]
        public Tienda Tienda { get; set; }

        //relacion-categoria
        public int categoriaId { get; set; }
        [ForeignKey("categoriaId")]
        public Categoria Categoria { get; set; }

    }
}
