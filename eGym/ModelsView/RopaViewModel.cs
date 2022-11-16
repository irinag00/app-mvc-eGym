using eGym.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eGym.ModelsView
{
    public class RopaViewModel
    {
        public List<Ropa> ListaRopa { get; set; }
        public SelectList ListaCategoria { get; set; }
        public int? categoriaId { get; set; }
        public string busqNombre { get; set; }
        public paginador paginador { get; set; }/* = new paginador();*/
        public bool busqueda { get; set; }
    }

    public class paginador
    {
        public int PaginaActual { get; set; }
        public int CantidadRegistros { get; set; }
        public int RegistrosxPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((decimal) CantidadRegistros / RegistrosxPagina);
        public Dictionary<string, string> ValoresQueryString { get; set; } = new Dictionary<string, string>();
    }

}
