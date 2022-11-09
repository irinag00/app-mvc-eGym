namespace eGym.Models
{
    public class Color
    {
        public int idColor { get; set; }
        public string nombre { get; set; }
        public string imagenColor { get; set;}

        //relaciones
        public List<Ropa_Color> ropas_colores { get; set; }
    }
}
