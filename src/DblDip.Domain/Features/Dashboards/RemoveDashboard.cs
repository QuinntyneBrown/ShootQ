using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class RemoveDashboard
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {

            }
        }

        public class Request : IRequest<Unit>
        {
            public Guid DashboardId { get; init; }
        }

        public class Response: ResponseBase
        {
            public DashboardDto Dashboard { get; init; }
        }

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IEventStore _store;
            private readonly IDateTime _dateTime;

            public Handler(IEventStore store, IDateTime dateTime)
            {
                _store = store;
                _dateTime = dateTime;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var dashboard = await _store.FindAsync<Dashboard>(request.DashboardId);

                dashboard.Remove(_dateTime.UtcNow);

                _store.Add(dashboard);

                await _store.SaveChangesAsync(cancellationToken);

                return new();
            }
        }
    }
}
