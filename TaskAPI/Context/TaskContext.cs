using Microsoft.EntityFrameworkCore;
using TaskAPI.Model;
namespace TaskAPI.Context
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }
        public DbSet<Tasks> Tasks { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Tasks>().ToTable("Tarefas"); 
        }


    }
}
