using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Pipelines.BuildingBlocks
{
    public static class OrderForProcessingExtensions
	{
        public static OrderForProcessing Merge(this OrderForProcessing existing, Store? _store) =>
            new OrderForProcessing
            {
                StoreHours = existing.StoreHours,
                CustomerSavedPayment = existing.CustomerSavedPayment,
                Store = _store,
                ShoppingCart = existing.ShoppingCart
            };

        public static OrderForProcessing Merge(this OrderForProcessing existing, StoreHours? _storeHours) =>
            new OrderForProcessing
            {
                StoreHours = _storeHours,
                CustomerSavedPayment = existing.CustomerSavedPayment,
                Store = existing.Store,
                ShoppingCart = existing.ShoppingCart
            };

        public static OrderForProcessing Merge(this OrderForProcessing existing, CustomerSavedPayment? _customerSavedPayment) =>
            new OrderForProcessing
            {
                StoreHours = existing.StoreHours,
                CustomerSavedPayment = _customerSavedPayment,
                Store = existing.Store,
                ShoppingCart = existing.ShoppingCart
            };

        public static OrderForProcessing Merge(this OrderForProcessing existing, ShoppingCart? _shoppingCart) =>
            new OrderForProcessing
            {
                StoreHours = existing.StoreHours,
                CustomerSavedPayment = existing.CustomerSavedPayment,
                Store = existing.Store,
                ShoppingCart = _shoppingCart
            };
    }
}

