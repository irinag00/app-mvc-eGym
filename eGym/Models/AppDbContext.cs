using Microsoft.EntityFrameworkCore;

namespace eGym.Models
{
    public class AppDbContext : DbContext
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
