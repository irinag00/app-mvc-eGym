using Microsoft.EntityFrameworkCore;

namespace eGym.Models
{
    public class AppDbContext : DbContext
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ropa_Color>().HasKey(am => new
            {
                am.idRopa,
                am.idColor
            });

            modelBuilder.Entity<Ropa_Color>().HasOne(m => m.ropa).WithMany(am => am.ropas_colores)
                .HasForeignKey(m => m.idRopa);

            modelBuilder.Entity<Ropa_Color>().HasOne(m => m.color).WithMany(am => am.ropas_colores)
                .HasForeignKey(m => m.idColor);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Ropa> Ropas { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Ropa_Color> Ropa_Colores { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
    }
}
