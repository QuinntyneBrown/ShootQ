using BuildingBlocks.Core;
using DblDip.Core.Data;
using DblDip.Core.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class GetPortraitById
    {
        public class Request : IRequest<Response>
        {
            public Guid PortraitId { get; init; }
        }

        public class Response: ResponseBase
        {
            public PortraitDto Portrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var portrait = await _context.FindAsync<Portrait>(request.PortraitId);

                return new Response()
                {
                    Portrait = portrait.ToDto()
                };
            }
        }
    }
}
