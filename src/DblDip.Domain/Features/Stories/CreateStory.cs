using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateStory
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Story).NotNull();
                RuleFor(request => request.Story).SetValidator(new StoryValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public StoryDto Story { get; init; }
        }

        public class Response
        {
            public StoryDto Story { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var story = new Story();

                _context.Add(story);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Story = story.ToDto()
                };
            }
        }
    }
}
