using TylersPizzaChain.Exceptions;
using TylersPizzaChain.Models;
using TylersPizzaChain.Pipelines.BuildingBlocks;
using TylersPizzaChain.Pipelines.BuildingBlocks.Clients;
using TylersPizzaChain.Pipelines.BuildingBlocks.DatabaseQueries;

namespace TylersPizzaChain.Pipelines
{
    public static class ProcessOrderPipeline
    {
        public static async Task<OrderConfirmation> Execute(ImpureDependencies impure, OrderDetails input)
        {
            //throw new NotImplementedException();

            //look up store
            var store = await StoreQueries.GetStoreById(impure._dbContext, input.StoreId);
            if (store == null) { throw new OrderProcessingException("Store not found"); }

            var storeHours = await StoreQueries.GetStoreHours(impure._dbContext, input.StoreId);
            if (storeHours == null) { throw new OrderProcessingException("Store Hours not found"); }

            //validate store hours with order time
            var isOrderTimeValid = StoreHoursValidation.ValidateOrderTime(impure._configuration, input, storeHours, store.TimeZone);
            if (!isOrderTimeValid) { throw new OrderProcessingException("Invalid store time"); }

            //look up shopping cart
            var shoppingCart = await ShoppingCartQueries.GetShoppingCartById(impure._dbContext, input.ShoppingCartId);
            if (shoppingCart == null) { throw new OrderProcessingException("Shopping Cart not found"); }

            //validate shopping cart
            var isSHoppingCardValid = await ShoppingCartQueries.ValidateShoppingCartToStore(impure._dbContext, input.ShoppingCartId, store);
            if (!isSHoppingCardValid) { throw new OrderProcessingException("Shopping Cart is not valid"); }//TODO: return invalid items

            //calculate order total
            var totalTax = Math.Round(shoppingCart.MenuItems?.Aggregate(0M, (acc, val) => acc + (val.Price * val.TaxRate)) ?? 0, 2);
            var subtotal = shoppingCart.MenuItems?.Aggregate(0M, (acc, val) => acc + val.Price) ?? 0;
            var orderTotal = subtotal + totalTax; //TODO: add delivery fee if applicable


            //lookup saved payment
            var savedPayment = await CustomerQueries.GetCustomerSavedPayment(impure._dbContext, input.CustomerId, input.PaymentId);
            if (savedPayment == null) { throw new OrderProcessingException("Saved Payment not found"); }

            //process payment
            var paymentResponse = await PaymentProcessorClient.ProcessPayment(impure._httpClientFactory, savedPayment.VendorPaymentId, orderTotal);
            if (!paymentResponse.IsSuccess) { throw new OrderProcessingException("Payment failed"); }//TODO: parse why payment failed to notify customer

            //send to point of sale
            //send to point of sale
            var posResponse = await PointOfSaleClient.SendToPointOfSale(impure._httpClientFactory, input, shoppingCart);
            if (!posResponse.IsSuccess) { throw new OrderProcessingException("Error sending to point of sale"); }//perform cleanup on order

            //if delivery, send to delivery service
            if (input.OrderType == OrderType.Delivery)
            {
                //send to a delivery vendor
                var deliveryResponse = await DeliveryCoordinator.SendOrderToDeliveryProvider(impure._httpClientFactory, input, shoppingCart, store, orderTotal);
                if (!deliveryResponse.IsSuccess) { throw new OrderProcessingException("Could not notify deliver provider"); }
            }

            //send back confirmation
            return new OrderConfirmation() { ConfirmationNumber = "1234", Message = "Order Processed.", Processed = true, ShoppingCardId = input.ShoppingCartId };
        }
    }
}

