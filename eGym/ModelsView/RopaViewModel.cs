using eGym.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGym.ModelsView
{
    public class RopaViewModel
    {
        public List<Ropa> ListaRopa { get; set; }
        public SelectList ListaCategoria { get; set; }
        public string busqNombre { get; set; }
        public paginador paginador { get; set; }
    }

    public class paginador
    {
        public int cantReg { get; set; }
        public int regXpag { get; set; }
        public int pagActual { get; set; }
        public int totalPag => (int)Math.Ceiling((decimal)cantReg / regXpag);
        public Dictionary<string, string> ValoresQueryString { get; set; } = new Dictionary<string, string>();
    }

}
