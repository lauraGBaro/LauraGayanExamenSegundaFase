using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Divisa> Divisas { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        public MyDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=DatabaseVentas.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });


            base.OnConfiguring(optionsBuilder);
        }
    }
}
