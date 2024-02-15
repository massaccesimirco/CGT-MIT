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
    }
}
