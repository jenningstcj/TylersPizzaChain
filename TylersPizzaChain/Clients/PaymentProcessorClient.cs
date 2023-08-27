using System.Text;
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

        public async Task<PaymentResponse> ProcessPayment(string vendorPaymentId, decimal amount)
        {
            var httpContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new PaymentVendorChargeRequest { AmountInCents = amount * 100, PaymentId = vendorPaymentId }),
                UnicodeEncoding.UTF8,
                "application/json"
                );
            var httpResponse = await _httpClient.PostAsync("/card/charge", httpContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                string content = await httpResponse.Content.ReadAsStringAsync();
                var responseData = System.Text.Json.JsonSerializer.Deserialize<PaymentVendorChargeResponse>(content);
                return new PaymentResponse() { IsSuccess = responseData?.IsSuccess ?? false, AmountCharged = amount };
            }
            else
            {
                return new PaymentResponse() { AmountCharged = 0M, IsSuccess = false };
            }
        }
    }
}

