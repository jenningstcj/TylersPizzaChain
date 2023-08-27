using System;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Exceptions;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Services
{
    public interface IStoreHoursValidationService
    {
        bool ValidateOrderTime(OrderDetails orderDetails, StoreHours storeHours, string timeZone);
    }

    public class StoreHoursValidationService : IStoreHoursValidationService
    {
        private readonly IConfiguration _configuration;
        private readonly Int32 Pickup_LeadTime;//time customer expects to pickup food in store
        private readonly Int32 EatIn_LeadTime;//time customer expects to pickup food in store
        private readonly Int32 Delivery_LeadTime;//time customer expects to recieve food delivered, must include time for delivery

        public StoreHoursValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
            Pickup_LeadTime = _configuration.GetValue<int>("Options:LeadTimesInMinutes:Pickup");
            EatIn_LeadTime = _configuration.GetValue<int>("Options:LeadTimesInMinutes:EatIn");
            Delivery_LeadTime = _configuration.GetValue<int>("Options:LeadTimesInMinutes:Delivery");
        }

        public bool ValidateOrderTime(OrderDetails orderDetails, StoreHours storeHours, string timeZone)
        {
            //Validate that expected order time is at least current time plus order type lead time
            bool leadTimeValid = ValidateLeadTime(orderDetails, timeZone);

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

        private bool ValidateLeadTime(OrderDetails orderDetails, string timeZone)
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            var currentStoreTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz);
            var leadTime = orderDetails.OrderType switch
            {
                OrderType.Delivery => Delivery_LeadTime,
                OrderType.EatIn => EatIn_LeadTime,
                OrderType.Pickup => Pickup_LeadTime,
                _ => throw new OrderProcessingException($"Lead Time not found for {orderDetails.OrderType}")
            };

            var leadTimeValid = orderDetails.WhenCustomerExpectsFood > currentStoreTime.AddMinutes(leadTime);
            return leadTimeValid;
        }
    }
}

