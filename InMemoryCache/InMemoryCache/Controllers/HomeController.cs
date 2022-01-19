using InMemoryCache.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace InMemoryCache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _cache;
        private readonly string Cachekey = "_MyValue";
        public HomeController(ILogger<HomeController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {
            DateTime value;
            if (!_cache.TryGetValue(Cachekey, out value))
            {
                value = DateTime.Now;
                var cacheOption = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(15))
                    .RegisterPostEvictionCallback(CacheRemeveCallBack,this);
                _cache.Set(Cachekey, value, cacheOption);
            }

            return View(value);
        }


        private static void CacheRemeveCallBack(object key, object value,EvictionReason reason,
            object state)
        {
            ;
        }

        public IActionResult RemoveCache()
        {
            _cache.Remove(Cachekey);
            return View(nameof(Index));
        }

        public IActionResult GetCache()
        {
            var cacheValue = _cache.Get(Cachekey);
            return View(nameof(Index), cacheValue);
        }

        public IActionResult GetOrCreateCache()
        {
            var cacheValue = _cache.GetOrCreate(Cachekey, p =>
             {
                 p.SetSlidingExpiration(TimeSpan.FromSeconds(10));
                 return DateTime.Now;
             });
            return View(nameof(Index), cacheValue);
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