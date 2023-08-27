using System.Text;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Pipelines.BuildingBlocks.Clients
{
	public static class PaymentProcessorClient
	{
        public static async Task<PaymentResponse> ProcessPayment(IHttpClientFactory httpClientFactory, string vendorPaymentId, decimal amount)
        {
            var httpClient = httpClientFactory.CreateClient("PaymentProcessorClient");
            var httpContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new PaymentVendorChargeRequest { AmountInCents = amount * 100, PaymentId = vendorPaymentId }),
                UnicodeEncoding.UTF8,
                "application/json"
                );
            var httpResponse = await httpClient.PostAsync("/card/charge", httpContent);
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

