

namespace Core.Entities.OrderAggregate
{
    public class DeliveryMethod: BaseEntity
    {
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}