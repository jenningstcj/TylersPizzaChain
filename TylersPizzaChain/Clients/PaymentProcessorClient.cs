using System;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Clients
{
	public interface IPaymentProcessorClient
	{
        Task<PaymentResponse> ProcessPayment(string vendorPaymentId, decimal amount);
	}

	public class PaymentProcessorClient : IPaymentProcessorClient
	{
        private readonly HttpClient _httpClient;

        public PaymentProcessorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<PaymentResponse> ProcessPayment(string vendorPaymentId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}

