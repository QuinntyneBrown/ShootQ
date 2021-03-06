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
    public class GetShotListById
    {
        public class Request : IRequest<Response>
        {
            public Guid ShotListId { get; init; }
        }

        public class Response: ResponseBase
        {
            public ShotListDto ShotList { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var shotList = await _context.FindAsync<ShotList>(request.ShotListId);

                return new Response()
                {
                    ShotList = shotList.ToDto()
                };
            }
        }
    }
}
