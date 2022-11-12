using System.ComponentModel.DataAnnotations;

namespace eGym.Models
{
    public class Ropa_Color
    {
        public int idRopa { get; set; }
        public Ropa ropa { get; set; }
       
       public int idColor { get; set; }
       public Color color { get; set; }
    }
}
