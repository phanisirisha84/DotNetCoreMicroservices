namespace Ecommerce.Api.ProductsCore.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Database.Product, Models.Product>();
        }
    }
}
