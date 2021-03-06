using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateOffer
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Offer).NotNull();
                RuleFor(request => request.Offer).SetValidator(new OfferValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public OfferDto Offer { get; init; }
        }

        public class Response: ResponseBase
        {
            public OfferDto Offer { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var offer = await _store.FindAsync<Offer>(request.Offer.OfferId);

                offer.Update();

                _store.Add(offer);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Offer = offer.ToDto()
                };
            }
        }
    }
}
