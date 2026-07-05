using Microsoft.EntityFrameworkCore;
using ToyoCarsClients.Domain.Entities;

namespace ToyoCarsClients.Infraestructure.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Encuestas> Encuestas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            }).Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Encuestas>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Rating).IsRequired();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Comentario).IsRequired().HasMaxLength(500);
            }).Entity<Encuestas>();
        }

    }
}
