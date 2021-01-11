using DblDip.Core.Data;
using DblDip.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Features
{
    public class UpdateDiscount
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Discount).NotNull();
                RuleFor(request => request.Discount).SetValidator(new DiscountValidator());
            }
        }

        public class Request : IRequest<Response> {  
            public DiscountDto Discount { get; set; }
        }

        public class Response
        {
            public DiscountDto Discount { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDblDipDbContext _context;

            public Handler(IDblDipDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var discount = await _context.FindAsync<Discount>(request.Discount.DiscountId);

                discount.Update();

                _context.Add(discount);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    Discount = discount.ToDto()
                };
            }
        }
    }
}
