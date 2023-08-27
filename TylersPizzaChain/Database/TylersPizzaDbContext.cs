using Microsoft.EntityFrameworkCore;
using TylersPizzaChain.Database.Entities;

namespace TylersPizzaChain.Database
{
    public class TylersPizzaDbContext : DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemPrice> MenuItemPrices { get; set; }
        public DbSet<PricingTier> PricingTiers { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreHours> StoreHours { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CustomerSavedPayment> CustomerSavedPayments { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public TylersPizzaDbContext(DbContextOptions<TylersPizzaDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>()
                .HasMany(_ => _.MenuItemPrices)
                .WithMany(_ => _.MenuItems);

            modelBuilder.Entity<MenuItemPrice>();

            modelBuilder.Entity<PricingTier>();

            modelBuilder.Entity<Store>()
                .HasMany(_ => _.StoreHours)
                .WithOne(_ => _.Store)
                .HasForeignKey(_ => _.StoreId)
                .IsRequired();

            modelBuilder.Entity<StoreHours>();

            modelBuilder.Entity<ShoppingCart>();

            modelBuilder.Entity<CustomerSavedPayment>();

            modelBuilder.Entity<Customer>();

        }

    }
}

