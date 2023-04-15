namespace Ecommerce.Api.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Database.Order, Models.Order>();
            CreateMap<Database.OrderItem, Models.OrderItem>();
        }
    }
}
