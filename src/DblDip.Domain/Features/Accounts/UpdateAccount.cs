using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateAccount
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Account).NotNull();
                RuleFor(request => request.Account).SetValidator(new AccountValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public AccountDto Account { get; set; }
        }

        public class Response: ResponseBase
        {
            public AccountDto Account { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var account = await _store.FindAsync<Account>(request.Account.AccountId);

                account.Update();

                _store.Add(account);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Account = account.ToDto()
                };
            }
        }
    }
}
