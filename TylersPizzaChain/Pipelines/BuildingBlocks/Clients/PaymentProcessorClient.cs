using System.Text;
using TylersPizzaChain.Models;

namespace TylersPizzaChain.Pipelines.BuildingBlocks.Clients
{
    public static class PaymentProcessorClient
    {
        public static async Task<PaymentResponse> ProcessPayment(ILogger logger, IHttpClientFactory httpClientFactory, string vendorPaymentId, decimal amount)
        {
            var httpClient = httpClientFactory.CreateClient("PaymentProcessorClient");
            logger.LogInformation($"Payment processor base url: {httpClient.BaseAddress}");

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

        public static Func<string, decimal, Task<PaymentResponse>> ProcessPaymentPartiallyApplied(ILogger logger, IHttpClientFactory httpClientFactory)
        {
            return async (string vendorPaymentId, decimal amount)
            => await ProcessPayment(logger, httpClientFactory, vendorPaymentId, amount);
        }

            
    }
}

