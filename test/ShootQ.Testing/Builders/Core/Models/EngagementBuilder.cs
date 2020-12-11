using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class EngagementBuilder
    {
        private Engagement _engagement;

        public static Engagement WithDefaults()
        {
            return new Engagement();
        }

        public EngagementBuilder()
        {
            _engagement = WithDefaults();
        }

        public Engagement Build()
        {
            return _engagement;
        }
    }
}