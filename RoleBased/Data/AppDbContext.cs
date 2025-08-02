using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoleBased.Models;
namespace RoleBased.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } // Optional, if you want to access Users directly
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=RoleBased;integrated security=sspi;trustservercertificate=true;encrypt=true");
        }

        
    }
}
