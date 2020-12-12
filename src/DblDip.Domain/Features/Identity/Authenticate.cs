using BuildingBlocks.Abstractions;
using BuildingBlocks.Core;
using FluentValidation;
using MediatR;
using DblDip.Core;
using DblDip.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features.Identity
{
    public class Authenticate
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Username).NotNull();
                RuleFor(x => x.Password).NotNull();
            }
        }

        public class Request : IRequest<Response>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public string AccessToken { get; set; }
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
            private readonly ITokenProvider _tokenProvider;

            public Handler(IAppDbContext context, ITokenProvider tokenProvider, IPasswordHasher passwordHasher)
            {
                _context = context;
                _tokenProvider = tokenProvider;
                _passwordHasher = passwordHasher;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var user = _context.Set<User>()
                    .SingleOrDefault(x => ((string)x.Username).ToLower() == request.Username.ToLower());

                if (user == null)
                    throw new Exception();

                if (!ValidateUser(user, _passwordHasher.HashPassword(user.Salt, request.Password)))
                    throw new Exception();

                var roles = user?.Roles.Select(x => new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", x.Name)).ToArray();

                var userIdClaim = new Claim(Constants.ClaimTypes.UserId, $"{user.UserId}");

                return new Response()
                {

                    AccessToken = _tokenProvider.Get(request.Username, new List<Claim> { userIdClaim }),
                    UserId = user.UserId
                };
            }

            public bool ValidateUser(User user, string transformedPassword)
            {
                if (user == null || transformedPassword == null)
                    return false;

                return user.Password == transformedPassword;
            }
        }
    }
}