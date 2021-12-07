using Microsoft.EntityFrameworkCore;
using MUSACA.Constants;
using MUSACA.Data.Models;

namespace MUSACA.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configurations.DbConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .Property(x => x.Barcode)
                .HasMaxLength(12)
                .IsFixedLength(true);

            modelBuilder
                .Entity<Order>(e =>
                {
                    e.HasOne(x => x.Product)
                        .WithMany(x => x.Orders);

                    e.HasOne(x => x.Receipt)
                        .WithMany(x => x.Orders);
                });

            modelBuilder
               .Entity<Receipt>()
               .HasOne(x => x.Cashier)
                       .WithMany(x => x.Receipts);
        }
    }
}
