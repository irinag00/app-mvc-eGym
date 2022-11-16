using eGym.Models;

namespace eGym.ModelsView
{
    public class MarcaViewModel
    {
        public List<Marca> ListaMarca { get; set; }
        public string busqNombre { get; set; }
        public paginador paginador { get; set; }
    }
}
