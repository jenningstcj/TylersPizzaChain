using System;
namespace TylersPizzaChain.Exceptions
{
    public class OrderProcessingException : Exception
    {
        public OrderProcessingException()
        {
        }

        public OrderProcessingException(string message)
            : base(message)
        {
        }

        public OrderProcessingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

