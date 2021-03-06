using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateBrand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Brand).NotNull();
                RuleFor(request => request.Brand).SetValidator(new BrandValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public BrandDto Brand { get; init; }
        }

        public class Response: ResponseBase
        {
            public BrandDto Brand { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var brand = new Brand();

                _store.Add(brand);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Brand = brand.ToDto()
                };
            }
        }
    }
}
