using RedisCache.Models.Dto;

namespace RedisCache.Models
{
    public interface IProductServices
    {
        HomePageDto GetHomePageProducts();
    }

    public class ProductRepasitory : IProductServices
    {
        public HomePageDto GetHomePageProducts()
        {
            Thread.Sleep(3000);
            return new HomePageDto()
            {
                BestProduct = new BestProduct()
                {
                    Products = new List<ProductDto>()
                      {
                           new ProductDto { Id=1, Name="آموزش اصول  Solid"},
                           new ProductDto { Id=2, Name="آموزش الگوهای طراحی"}
                      }
                },
                LastProduct = new LastProduct()
                {
                    Products = new List<ProductDto>()
                      {
                           new ProductDto { Id=3, Name="آموزش میکروسرویس"},
                           new ProductDto { Id=4, Name="آموزش DDD"},
                           new ProductDto { Id=5, Name="آموزش پیشرفته سی شارپ"}
                      }
                }
            };
        }
    }
}
