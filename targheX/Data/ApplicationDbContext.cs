using Microsoft.EntityFrameworkCore;
using targheX.Models;

namespace targheX.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazione delle proprietà aggiunte
            modelBuilder.Entity<Item>()
                .Property(i => i.Year)
                .IsRequired();

            modelBuilder.Entity<Item>()
                .Property(i => i.IsClosed)
                .HasDefaultValue(false);
        }
    }
}
