using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace eGym.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string nombre { get; set; }
    }
}
