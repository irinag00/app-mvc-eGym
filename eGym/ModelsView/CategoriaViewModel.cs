using eGym.Models;

namespace eGym.ModelsView
{
    public class CategoriaViewModel
    {
        public List<Categoria> ListaCategoria { get; set; }
        public string busqNombre { get; set; }
        public paginador paginador { get; set; }

    }
}