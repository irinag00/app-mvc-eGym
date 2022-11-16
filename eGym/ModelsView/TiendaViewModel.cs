using eGym.Models;

namespace eGym.ModelsView
{
    public class TiendaViewModel
    {
        public List<Tienda> ListaTienda { get; set; }
        public string busqNombre { get; set; }
        public paginador paginador { get; set; }
    }
}
