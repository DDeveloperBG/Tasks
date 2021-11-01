using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Sales;Integrated Security=true");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .Property(x => x.Email)
                .IsUnicode(false);

            modelBuilder
              .Entity<Sale>(e =>
              {
                  e.HasOne(x => x.Product)
                  .WithMany(x => x.Sales);

                  e.HasOne(x => x.Customer)
                  .WithMany(x => x.Sales);

                  e.HasOne(x => x.Store)
                  .WithMany(x => x.Sales);

                  e.Property(x => x.Date)
                  .HasDefaultValueSql("GETDATE()");
              });

            modelBuilder
                .Entity<Product>()
                .Property(x => x.Description)
                .HasDefaultValue("No description");

            base.OnModelCreating(modelBuilder);
        }
    }
}
