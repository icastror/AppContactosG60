using Microsoft.EntityFrameworkCore;
using AppContactosG60.Models;

namespace AppContactosG60.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<Contacto>? Contacto {get; set; }
    }
}