using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Api.Data.Map;
using ProjetoMVC.Api.Models;

namespace ProjetoMVC.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ContatoModel> Contatos { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
