using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateProjectManager
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProjectManager).NotNull();
                RuleFor(request => request.ProjectManager).SetValidator(new ProjectManagerValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ProjectManagerDto ProjectManager { get; init; }
        }

        public class Response
        {
            public ProjectManagerDto ProjectManager { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var projectManager = await _context.FindAsync<ProjectManager>(request.ProjectManager.ProjectManagerId);

                projectManager.Update();

                _context.Add(projectManager);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    ProjectManager = projectManager.ToDto()
                };
            }
        }
    }
}
