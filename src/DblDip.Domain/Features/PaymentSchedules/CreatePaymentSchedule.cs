using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class CreatePaymentSchedule
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.PaymentSchedule).NotNull();
                RuleFor(request => request.PaymentSchedule).SetValidator(new PaymentScheduleValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public PaymentScheduleDto PaymentSchedule { get; set; }
        }

        public class Response: ResponseBase
        {
            public PaymentScheduleDto PaymentSchedule { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _store;

            public Handler(IEventStore store) => _store = store;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var paymentSchedule = new PaymentSchedule();

                _store.Add(paymentSchedule);

                await _store.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    PaymentSchedule = paymentSchedule.ToDto()
                };
            }
        }
    }
}
