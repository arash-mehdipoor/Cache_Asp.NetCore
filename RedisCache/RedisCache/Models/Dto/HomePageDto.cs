namespace RedisCache.Models.Dto
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class BestProduct
    {
        public List<ProductDto> Products { get; set; }
    }
    public class LastProduct
    {
        public List<ProductDto> Products { get; set; }
    }
    public class HomePageDto
    {
        public BestProduct BestProduct { get; set; }
        public LastProduct LastProduct { get; set; }
    }
}
