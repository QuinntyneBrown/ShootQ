using BuildingBlocks.Abstractions;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.Models
{
    public class Equipment : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid EquipmentId { get; private set; }
        public string Name { get; private set; }
        public Price Price { get; private set; }
        public string Description { get; private set; }
        public Guid? ReceiptDigitalAssetId { get; private set; }
    }
}
