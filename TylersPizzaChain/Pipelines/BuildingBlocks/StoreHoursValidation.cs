using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Exceptions;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Pipelines.BuildingBlocks
{
    public static class StoreHoursValidation
	{
        public static bool ValidateOrderTime(IConfiguration configuration, OrderDetails orderDetails, StoreHours storeHours, string timeZone)
        {
            //Validate that expected order time is at least current time plus order type lead time
            bool leadTimeValid = ValidateLeadTime(configuration, orderDetails, timeZone);

            //check if store is open at expected time
            bool storeIsOpen = ValidateStoreIsOpen(orderDetails, storeHours);

            return leadTimeValid && storeIsOpen;
        }

        private static bool ValidateStoreIsOpen(OrderDetails orderDetails, StoreHours storeHours)
        {
            //For simplicity, only using hours current as of today.
            //TODO: extend to lookup future hours if store hours are set to change on a future date
            var hoursForExpectedDay = storeHours.HoursList
                .Where(_ => _.DayOfWeek == orderDetails.WhenCustomerExpectsFood.DayOfWeek)
                .FirstOrDefault() ?? new DayHours { OpenTime = new TimeSpan(8, 0, 0), CloseTime = new TimeSpan(20, 0, 0) };//default

            var storeIsOpen = orderDetails.WhenCustomerExpectsFood.TimeOfDay > hoursForExpectedDay.OpenTime
                && orderDetails.WhenCustomerExpectsFood.TimeOfDay < hoursForExpectedDay.CloseTime;
            return storeIsOpen;
        }

        private static bool ValidateLeadTime(IConfiguration configuration, OrderDetails orderDetails, string timeZone)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            var currentStoreTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            var leadTime = orderDetails.OrderType switch
            {
                OrderType.Delivery => configuration.GetLeadTimeInMinutes_Delivery(),
                OrderType.EatIn => configuration.GetLeadTimeInMinutes_EatIn(),
                OrderType.Pickup => configuration.GetLeadTimeInMinutes_Pickup(),
                _ => throw new OrderProcessingException($"Lead Time not found for {orderDetails.OrderType}")
            };

            var leadTimeValid = orderDetails.WhenCustomerExpectsFood > currentStoreTime.AddMinutes(leadTime);
            return leadTimeValid;
        }
    }
}

