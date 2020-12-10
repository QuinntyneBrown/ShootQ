using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class OrderBuilder
    {
        private Order _order;

        public static Order WithDefaults() => new Order();

        public OrderBuilder()
        {
            _order = new Order();
        }

        public Order Build()
        {
            return _order;
        }
    }
}
