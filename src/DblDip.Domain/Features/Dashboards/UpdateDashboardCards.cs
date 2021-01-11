using DblDip.Core.Data;
using MediatR;
using DblDip.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static DblDip.Core.Models.Dashboard;

namespace DblDip.Domain.Features
{
    public class UpdateDashboardCards
    {
        public record Request(Guid DashboardId, ICollection<DashboardCardDto> DashboardCards) : IRequest<Response>;

        public record Response(DashboardDto Dashboard);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context)
                => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = await _context.FindAsync<Dashboard>(request.DashboardId);

                var dashboardCards = request.DashboardCards.Select(x => new DashboardCard(x.DashboardCardId, x.Options)).ToList();

                dashboard.UpdateDashboardCards(dashboardCards);

                _context.Add(dashboard);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response(dashboard.ToDto());
            }
        }
    }
}
