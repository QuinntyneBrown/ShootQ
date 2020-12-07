using BuildingBlocks.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using ShootQ.Core;
using ShootQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Dashboards
{
    public class GetDashboardsByCurrentUser
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<DashboardDto> Dashboards { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(IAppDbContext context, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var currentUserId = new Guid(_httpContextAccessor.HttpContext.User
                    .FindFirst(Constants.ClaimTypes.UserId).Value);

                var dashboards = _context.Set<Dashboard>().Where(x => x.UserId == currentUserId);

                return new Response()
                {
                    Dashboards = _context.Set<Dashboard>().Select(x => x.ToDto()).ToList()
                };
            }
        }
    }
}