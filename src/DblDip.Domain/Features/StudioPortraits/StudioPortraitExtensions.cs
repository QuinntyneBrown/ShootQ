using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
{
    public static class StudioPortraitExtensions
    {
        public static StudioPortraitDto ToDto(this StudioPortrait studioPortrait)
        {
            return new StudioPortraitDto(studioPortrait.StudioPortraitId);
        }
    }
}
