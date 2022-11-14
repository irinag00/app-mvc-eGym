using System.ComponentModel.DataAnnotations;

namespace eGym.Models
{
    public class Ropa_Color
    {
        public int idRopa { get; set; }
        [Display(Name = "Ropa")]
        public Ropa ? ropa { get; set; }
       
       public int idColor { get; set; }
        [Display(Name = "Color")]
        public Color ? color { get; set; }
    }
}
