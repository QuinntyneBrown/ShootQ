using DblDip.Domain.Features;

namespace DblDip.Testing.Builders
{
    public class SocialEventDtoBuilder
    {
        private SocialEventDto _socialEventDto;

        public static SocialEventDto WithDefaults()
        {
            return new SocialEventDto();
        }

        public SocialEventDtoBuilder()
        {
            _socialEventDto = new SocialEventDto();
        }

        public SocialEventDto Build()
        {
            return _socialEventDto;
        }
    }
}
