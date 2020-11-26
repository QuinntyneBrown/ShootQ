using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Customer: AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid CustomerId { get; private set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
