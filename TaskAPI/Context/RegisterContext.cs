using Microsoft.EntityFrameworkCore;
using TaskAPI.Model;

namespace TaskAPI.Context
{
    public class RegisterContext : DbContext
    {
        public RegisterContext(DbContextOptions<RegisterContext> options) : base(options)
        {
        }
        public DbSet<Register> Login { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Register>().ToTable("Login");
        }
    }

}

