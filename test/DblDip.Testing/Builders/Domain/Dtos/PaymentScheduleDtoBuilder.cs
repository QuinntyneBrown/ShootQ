using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class PaymentScheduleDtoBuilder
    {
        private PaymentScheduleDto _paymentScheduleDto;

        public static PaymentScheduleDto WithDefaults()
        {
            return new PaymentScheduleDto();
        }

        public PaymentScheduleDtoBuilder()
        {
            _paymentScheduleDto = WithDefaults();
        }

        public PaymentScheduleDto Build()
        {
            return _paymentScheduleDto;
        }
    }
}
