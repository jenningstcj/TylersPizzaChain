using System;
using TylersPizzaChain.Database.Entities;

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
            }
            catch (Exception ex)
            {
                throw;
            }

            return app;
        }
    }

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
            PricingTier = 1,
            TaxRate = 0.07M
        };

        public static StoreHours StoreHours = new StoreHours
        {
            Id = 1,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            LastUpdatedBy = Guid.NewGuid(),
            EffectiveDate = DateTime.UtcNow.AddDays(-1),
            StoreId = 1,
            Hours = new List<DayHours>
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
            }
        };
    }
}

