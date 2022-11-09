namespace eGym.Models
{
    public class Categoria
    {
        public int idCategoria { get; set; }
        public string nombre { get; set; }

        //relaciones
        public List<Ropa> Ropas { get; set; }
    }
}
