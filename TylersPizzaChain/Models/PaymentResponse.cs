using System;
namespace TylersPizzaChain.Models
{
	public class PaymentResponse
	{
		public bool IsSuccess { get; set; }
		public decimal AmountCharged { get; set; }
	}
}

