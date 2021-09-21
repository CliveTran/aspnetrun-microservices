using order.domain.Entities;

namespace order.application.Features.Orders.Queries
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public decimal TotalPrice { get; set; }

        // BillingAddress
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string AddressLine { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        // Payment
        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public string Expiration { get; set; }

        public string CVV { get; set; }

        public int PaymentMethod { get; set; }

        public OrderViewModel(Order order)
        {
            Id = order.Id;
            UserName = order.UserName;
            TotalPrice = order.TotalPrice;
            FirstName = order.FirstName;
            LastName = order.LastName;
            EmailAddress = order.EmailAddress;
            AddressLine = order.AddressLine;
            Country = order.Country;
            State = order.State;
            ZipCode = order.ZipCode;
            CardName = order.CardName;
            CardNumber = order.CardNumber;
            Expiration = order.Expiration;
            CVV = order.CVV;
            PaymentMethod = order.PaymentMethod;
        }
    }
}
