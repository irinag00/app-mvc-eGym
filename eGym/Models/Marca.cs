namespace eGym.Models
{
    public class Marca
    {
        public int idMarca { get; set; }
        public string nombre { get; set; }
        public string resumen { get; set; }
        public string imagenMarca { get; set; }

        //relaciones
        public List<Ropa> Ropas { get; set; }
    }
}
