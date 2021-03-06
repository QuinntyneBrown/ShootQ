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
    public class GetOrderById
    {
        public class Request : IRequest<Response>
        {
            public Guid OrderId { get; init; }
        }

        public class Response: ResponseBase
        {
            public OrderDto Order { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var order = await _context.FindAsync<Order>(request.OrderId);

                return new Response()
                {
                    Order = order.ToDto()
                };
            }
        }
    }
}
