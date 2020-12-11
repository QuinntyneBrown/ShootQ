using BuildingBlocks.Abstractions;
using ShootQ.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShootQ.Domain.Features.Posts
{
    public class UpdatePost
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Post).NotNull();
                RuleFor(request => request.Post).SetValidator(new PostValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public PostDto Post { get; set; }
        }

        public class Response
        {
            public PostDto Post { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var post = await _context.FindAsync<Post>(request.Post.PostId);

                //post.Update();

                _context.Store(post);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Post = post.ToDto()
                };
            }
        }
    }
}