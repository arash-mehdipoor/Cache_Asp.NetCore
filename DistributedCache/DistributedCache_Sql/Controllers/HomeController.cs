using DistributedCache_Sql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Text;

namespace DistributedCache_Sql.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache _cache;

        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {
            var model = string.Empty;
            var cacheValueEncoded = _cache.Get("CacheKey");
            if (cacheValueEncoded != null)
            {
                model = Encoding.UTF8.GetString(cacheValueEncoded);
            }
            else
            {
                model = DateTime.Now.ToString();
                byte[] currentValueEncoded = Encoding.UTF8.GetBytes(model);
                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(15));

                _cache.Set("CacheKey", currentValueEncoded, options);

            }
            return View("Index",model);
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