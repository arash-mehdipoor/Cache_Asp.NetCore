using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisCache.Models;
using RedisCache.Models.Dto;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace RedisCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices _product;
        private readonly IDistributedCache _cache;
        public HomeController(ILogger<HomeController> logger, IProductServices product, IDistributedCache cache)
        {
            _logger = logger;
            _product = product;
            _cache = cache;
        }

        public IActionResult Index()
        {
            HomePageDto homePageData = new HomePageDto();

            var homePageCache = _cache.GetAsync("HomePageData").Result;
            if (homePageCache != null)
            {
                homePageData = JsonSerializer.Deserialize<HomePageDto>(homePageCache);
                ViewBag.From = "  از کش دریافت شد";
            }
            else
            {
                homePageData = _product.GetHomePageProducts();

                string jsonData = JsonSerializer.Serialize(homePageData);

                byte[] encodedJson = Encoding.UTF8.GetBytes(jsonData);

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                    ;
                _cache.SetAsync("HomePageData", encodedJson, options);
                ViewBag.From = "از دیتابیس دریافت شد";

            }
            return View(homePageData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}