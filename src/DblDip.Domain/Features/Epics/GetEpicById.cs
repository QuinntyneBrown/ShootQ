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
    public class GetEpicById
    {
        public class Request : IRequest<Response>
        {
            public Guid EpicId { get; init; }
        }

        public class Response: ResponseBase
        {
            public EpicDto Epic { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var epic = await _context.FindAsync<Epic>(request.EpicId);

                return new Response()
                {
                    Epic = epic.ToDto()
                };
            }
        }
    }
}
