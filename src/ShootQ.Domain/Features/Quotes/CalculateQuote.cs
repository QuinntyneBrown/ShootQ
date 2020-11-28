using BuildingBlocks.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Quotes
{
    public class CalculateQuote
    {
        public class Request : IRequest<Response> {
            public int Hours { get; set; }
        }

        public class Response
        {
            public decimal Total { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) {            
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                return new Response()
                {

                };
            }
        }
    }
}
