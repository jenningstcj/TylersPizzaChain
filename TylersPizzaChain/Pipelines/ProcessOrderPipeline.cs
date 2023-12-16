using System.Diagnostics;
using TylersPizzaChain.Exceptions;
using TylersPizzaChain.Models;
using TylersPizzaChain.Pipelines.BuildingBlocks;
using TylersPizzaChain.Pipelines.BuildingBlocks.Clients;
using TylersPizzaChain.Pipelines.BuildingBlocks.CompositionHelpers;
using TylersPizzaChain.Pipelines.BuildingBlocks.DatabaseQueries;

namespace TylersPizzaChain.Pipelines
{
    public static class ProcessOrderPipeline
    {
        public static async Task<OrderConfirmation> Execute(ImpureDependencies impure, OrderDetails input) =>
         (await StoreQueries.GetStoreById(impure._dbContext, input.StoreId))
                .GuardAgainstNull(() => new OrderProcessingException("Store not found"))
                .Then(store => new OrderForProcessing().Merge(store))
                .ThenAsync(async ofp => (await StoreQueries.GetStoreHours(impure._dbContext, input.StoreId))
                                            .GuardAgainstNull(() => new OrderProcessingException("Store Hours not found"))
                                            .Then(storeHours => ofp.Merge(storeHours)))
                .Then(ofp => StoreHoursValidation.ValidateOrderTime(impure._configuration, input, ofp.StoreHours!, ofp.Store!.TimeZone)
                                .GuardAgainstFalse(() => new OrderProcessingException("Invalid store time"))
                                .Then(valid => ofp))
                .ThenAsync(async ofp => (await ShoppingCartQueries.GetShoppingCartById(impure._dbContext, input.ShoppingCartId))
                                        .GuardAgainstNull(() => new OrderProcessingException("Shopping Cart not found"))
                                        .Then(shoppingCart => ofp.Merge(shoppingCart)))
                .ThenAsync(async ofp => (await ShoppingCartQueries.ValidateShoppingCartToStore(impure._dbContext, input.ShoppingCartId, ofp.Store!))
                                        .GuardAgainstFalse(() => new OrderProcessingException("Shopping Cart is not valid"))
                                        .Then(valid => ofp))
                .ThenAsync(async ofp => (await CustomerQueries.GetCustomerSavedPayment(impure._dbContext, input.CustomerId, input.PaymentId))
                                        .GuardAgainstNull(() => new OrderProcessingException("Saved Payment not found"))
                                        .Then(savedPayment => ofp.Merge(savedPayment)))
                .ThenAsync(async ofp => (await PaymentProcessorClient.ProcessPayment(impure._logger, impure._httpClientFactory, ofp.CustomerSavedPayment!.VendorPaymentId, ofp.OrderTotal))
                                        .GuardAgainstFalse(_ => _.IsSuccess, () => new OrderProcessingException("Payment failed"))
                                        .Then(valid => ofp))
                .ThenAsync(async ofp => (await PointOfSaleClient.SendToPointOfSale(impure._httpClientFactory, input, ofp.ShoppingCart!))
                                        .GuardAgainstFalse(_ => _.IsSuccess, () => new OrderProcessingException("Error sending to point of sale"))
                                        .Then(valid => ofp))
                .ThenAsync(async ofp => await IfDeliverySendToProvider(ofp, impure, input))
                .Then(ofp => new OrderConfirmation() { ConfirmationNumber = "1234", Message = "Order Processed.", Processed = true, ShoppingCardId = input.ShoppingCartId });
           

        

        static async Task<OrderForProcessing> IfDeliverySendToProvider(OrderForProcessing ofp, ImpureDependencies impure, OrderDetails input)
        {
            if (input.OrderType == OrderType.Delivery)
            {
                //send to a delivery vendor
                var deliveryResponse = await DeliveryCoordinator.SendOrderToDeliveryProvider(impure._httpClientFactory, input, ofp.ShoppingCart, ofp.Store, ofp.OrderTotal);
                if (!deliveryResponse.IsSuccess) { throw new OrderProcessingException("Could not notify deliver provider"); }
            }
            return ofp;
        }
    }
}

