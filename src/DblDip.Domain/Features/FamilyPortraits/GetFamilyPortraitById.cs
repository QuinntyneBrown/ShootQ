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
    public class GetFamilyPortraitById
    {
        public class Request : IRequest<Response>
        {
            public Guid FamilyPortraitId { get; init; }
        }

        public class Response: ResponseBase
        {
            public FamilyPortraitDto FamilyPortrait { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var familyPortrait = await _context.FindAsync<FamilyPortrait>(request.FamilyPortraitId);

                return new Response()
                {
                    FamilyPortrait = familyPortrait.ToDto()
                };
            }
        }
    }
}
