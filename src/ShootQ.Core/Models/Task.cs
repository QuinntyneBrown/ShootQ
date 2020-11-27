using BuildingBlocks.Abstractions;
using System;

namespace ShootQ.Core.Models
{
    public class Task : AggregateRoot
    {
        protected override void When(dynamic @event) => When(@event);

        protected override void EnsureValidState()
        {

        }

        public Guid TaskId { get; private set; }
    }
}
