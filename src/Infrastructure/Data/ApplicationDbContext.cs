using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValue(string.Empty);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasDefaultValue(string.Empty);

                entity.Property(e => e.URL_Logo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValue(string.Empty);

                entity.Property(e => e.api_key)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValue(string.Empty);

                entity.Property(e => e.assistant_id)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValue(string.Empty);

                entity.Property(e => e.realm_id)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValue(string.Empty);

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .IsRequired(false);
            });
        }
    }
}