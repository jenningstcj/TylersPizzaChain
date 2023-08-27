using System;
using TylersPizzaChain.Clients;
using TylersPizzaChain.Database;
using TylersPizzaChain.Exceptions;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Services
{
	public interface IOrderService
	{
		Task<OrderConfirmation> ProcessOrder(OrderDetails orderDetails);

    }

	public class OrderService : IOrderService
	{
		private readonly IDeliveryCoordinatorService _deliveryCoordinatorService;
		private readonly IShoppingCartService _shoppingCartService;
		private readonly IStoreService _storeService;
		private readonly IPointOfSaleClient _pointOfSaleClient;
		private readonly IStoreHoursValidationService _storeHoursValidationService;
		private readonly IPaymentProcessorClient _paymentProcessorClient;
		private readonly ICustomerService _customerService;

		public OrderService(
			IDeliveryCoordinatorService deliveryCoordinatorService,
			IShoppingCartService shoppingCartService,
			IStoreService storeService,
			IPointOfSaleClient pointOfSaleClient,
			IStoreHoursValidationService storeHoursValidationService,
			IPaymentProcessorClient paymentProcessorClient,
			ICustomerService customerService)
		{
			_deliveryCoordinatorService = deliveryCoordinatorService;
			_shoppingCartService = shoppingCartService;
			_storeService = storeService;
			_pointOfSaleClient = pointOfSaleClient;
			_storeHoursValidationService = storeHoursValidationService;
			_paymentProcessorClient = paymentProcessorClient;
			_customerService = customerService;
		}

		public async Task<OrderConfirmation> ProcessOrder(OrderDetails orderDetails)
		{
			//look up store
			var store = await _storeService.GetStoreById(orderDetails.StoreId);
			if(store == null) { throw new OrderProcessingException("Store not found"); }


			//validate store hours with order time
			var latestStoreHours = await _storeService.GetStoreHours(store.Id);
			if(latestStoreHours == null) { throw new OrderProcessingException("Store Hours not found"); }

            var isOrderTimeValid = _storeHoursValidationService.ValidateOrderTime(orderDetails, latestStoreHours, store.TimeZone);
			if (!isOrderTimeValid) { throw new OrderProcessingException("Invalid store time"); }

			//look up shopping cart
			var shoppingCart = await _shoppingCartService.GetShoppingCartById(orderDetails.ShoppingCartId);
            if (shoppingCart == null) { throw new OrderProcessingException("Shopping Cart not found"); }

			//validate shopping cart
			var isSHoppingCardValid = await _shoppingCartService.ValidateShoppingCartToStore(orderDetails.ShoppingCartId, store);
            if (!isSHoppingCardValid) { throw new OrderProcessingException("Shopping Cart is not valid"); }//TODO: return invalid items

			//calculate order total
			var totalTax = shoppingCart.MenuItems?.Aggregate(0M, (acc, val) => acc + (val.Price * val.TaxRate)) ?? 0;
            var subtotal = shoppingCart.MenuItems?.Aggregate(0M, (acc, val) => acc + val.Price) ?? 0;
			var orderTotal = subtotal + totalTax; //TODO: add delivery fee if applicable

            //process payment
            var savedPayment = await _customerService.GetCustomerSavedPayment(orderDetails.CustomerId, orderDetails.PaymentId);
            if (savedPayment == null) { throw new OrderProcessingException("Saved Payment not found"); }
            var paymentResponse = await _paymentProcessorClient.ProcessPayment(savedPayment.VendorPaymentId, orderTotal);

			//send to point of sale
			var posResponse = await _pointOfSaleClient.SendToPointOfSale(orderDetails, shoppingCart);
			if (!posResponse.IsSuccess) { throw new OrderProcessingException("Error sending to point of sale"); }//perform cleanup on order

			if(orderDetails.OrderType == OrderType.Delivery)
			{
				//send to a delivery vendor
				var deliveryResponse = await _deliveryCoordinatorService.SendOrderToDeliveryProvider(orderDetails, shoppingCart);
			}

            //send back confirmation
			return new OrderConfirmation() { ConfirmationNumber = "1234", Message ="Order Processed.", Processed = true, ShoppingCardId = orderDetails.ShoppingCartId};
		}


	}
}

