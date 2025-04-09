using Microsoft.EntityFrameworkCore;
using TaskAPI.Model;
namespace TaskAPI.Context
{
    public class TarefasContext : DbContext
    {
        public TarefasContext(DbContextOptions<TarefasContext> options) : base(options)
        {
        }
        public DbSet<Tarefas> Tarefas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.Entity<Tarefas>().ToTable("Tarefa");


        }
    }
}
