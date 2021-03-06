using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreateExpense
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Expense).NotNull();
                RuleFor(request => request.Expense).SetValidator(new ExpenseValidator());
            }
        }

        public class Request : IRequest<Response>
        {
            public ExpenseDto Expense { get; init; }
        }

        public class Response: ResponseBase
        {
            public ExpenseDto Expense { get; init; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var expense = new Expense();

                _store.Add(expense);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Expense = expense.ToDto()
                };
            }
        }
    }
}
