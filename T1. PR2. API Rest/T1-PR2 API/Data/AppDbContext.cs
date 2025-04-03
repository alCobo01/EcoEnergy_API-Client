using Microsoft.AspNetCore.Identity.EntityFrameworkCore
using Microsoft.EntityFrameworkCore;
using T1_PR2_API.Models;

namespace T1_PR2_API.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<Game> Games { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
