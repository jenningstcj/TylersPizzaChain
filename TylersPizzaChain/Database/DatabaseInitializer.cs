using System;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Database
{
    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<TylersPizzaDbContext>();

                context.Stores.Add(SeedData.Store);
                context.SaveChanges();

                context.StoreHours.Add(SeedData.StoreHours);
                context.SaveChanges();

                context.Customers.Add(SeedData.Customer);
                context.SaveChanges();

                context.CustomerSavedPayments.Add(SeedData.CustomerSavedPayment);
                context.SaveChanges();

                context.MenuItems.Add(SeedData.MenuItem);
                context.SaveChanges();

                context.MenuItemPrices.Add(SeedData.MenuItemPrice);
                context.SaveChanges();

                context.PricingTiers.Add(SeedData.PricingTier);
                context.SaveChanges();

                context.ShoppingCarts.Add(SeedData.ShoppingCart);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return app;
        }
    }

    /*
     * Test ShoppingCart Id: ada109ae-7244-496c-80cd-0c7bc7f3abcc
     * Test Customer: 825a065b-3620-42af-bea2-935be9524f5c
     * Test Customer Saved Payment: 3e7a1033-4543-47d7-b3c7-62dbe3d79751
     * 
     */

    internal static class SeedData
    {
        public static Store Store = new Store
        {
            Id = 1,
            Name = "Test Store",
            Address1 = "123 Somewhere St.",
            City = "Nowhere",
            State = "ZZ",
            Zip = "12345",
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastUpdatedBy = Guid.NewGuid(),
            PricingTierId = 1,
            TaxRate = 0.07M,
            TimeZone = "US Eastern Standard Time"
        };

        public static StoreHours StoreHours = new StoreHours
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastUpdatedBy = Guid.NewGuid(),
            EffectiveDate = DateTime.UtcNow.AddDays(-2),
            StoreId = 1,
            Hours = System.Text.Json.JsonSerializer.Serialize(new List<DayHours>
            {
                new DayHours
                {
                    DayOfWeek = DayOfWeek.Sunday,
                    OpenTime = new TimeSpan(8,0,0),
                    CloseTime = new TimeSpan(20,0,0)
                },
                new DayHours
                {
                    DayOfWeek = DayOfWeek.Monday,
                    OpenTime = new TimeSpan(8,0,0),
                    CloseTime = new TimeSpan(20,0,0)
                },
                new DayHours
                {
                    DayOfWeek = DayOfWeek.Tuesday,
                    OpenTime = new TimeSpan(8,0,0),
                    CloseTime = new TimeSpan(20,0,0)
                },
                new DayHours
                {
                    DayOfWeek = DayOfWeek.Wednesday,
                    OpenTime = new TimeSpan(8,0,0),
                    CloseTime = new TimeSpan(20,0,0)
                },
                new DayHours
                {
                    DayOfWeek = DayOfWeek.Thursday,
                    OpenTime = new TimeSpan(8,0,0),
                    CloseTime = new TimeSpan(20,0,0)
                },
                new DayHours
                {
                    DayOfWeek = DayOfWeek.Friday,
                    OpenTime = new TimeSpan(8,0,0),
                    CloseTime = new TimeSpan(20,0,0)
                },
                new DayHours
                {
                    DayOfWeek = DayOfWeek.Saturday,
                    OpenTime = new TimeSpan(8,0,0),
                    CloseTime = new TimeSpan(20,0,0)
                },
            })
        };

        public static Customer Customer = new Customer()
        {
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastUpdatedBy = Guid.NewGuid(),
            FirstName = "Tyler",
            LastName = "Jennings",
            Id = Guid.Parse("825a065b-3620-42af-bea2-935be9524f5c")
        };

        public static CustomerSavedPayment CustomerSavedPayment = new CustomerSavedPayment
        {
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastUpdatedBy = Guid.NewGuid(),
            CardType = CardTypes.MasterCard,
            CustomerId = Guid.Parse("825a065b-3620-42af-bea2-935be9524f5c"),
            ExpirationMonth = 1,
            ExpirationYear = 2030,
            Id = Guid.Parse("3e7a1033-4543-47d7-b3c7-62dbe3d79751"),
            VendorPaymentId = "some-vendor-payment-id"
        };

        public static MenuItem MenuItem = new MenuItem
        {
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastUpdatedBy = Guid.NewGuid(),
            Id = 1,
            Description = "Pepperoni Pizza",
            Name = "Pepperoni Pizza"
        };

        public static MenuItemPrice MenuItemPrice = new MenuItemPrice
        {
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastUpdatedBy = Guid.NewGuid(),
            EffectiveDate = DateTime.UtcNow.AddDays(-5),
            Id = 1,
            MenuItemId = 1,
            Price = 12.50M,
            PricingTierId = 1
        };

        public static PricingTier PricingTier = new PricingTier
        {
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastUpdatedBy = Guid.NewGuid(),
            Id = 1,
            Name = "Tier 1"
        };

        public static ShoppingCart ShoppingCart = new ShoppingCart
        {
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastUpdatedBy = Guid.NewGuid(),
            Id = Guid.Parse("ada109ae-7244-496c-80cd-0c7bc7f3abcc"),
            CartDetails = System.Text.Json.JsonSerializer.Serialize(new List<MenuItemWithPrice>
            {
                new MenuItemWithPrice
                {
                    Id = 1,
                    Price = 12.50M,
                    Name = "Pepperoni Pizza",
                    PricingTierId = 1,
                    TaxRate = 0.07M
                }
            })
        };
    }
}

