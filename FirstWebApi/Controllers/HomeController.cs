using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;


namespace FirstWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // Getting data from the api using the HttpClient class
            using (HttpClient client = new HttpClient())
            {
                using HttpResponseMessage response = await client.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=Copenhagen,dk&APPID=7d625d703c019eeaf4cc874bd716bb0d");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializing JSON Data to C#- Objects. 
                var rootObject = JsonSerializer.Deserialize<Root>(responseBody);
                

                ViewData["Data"] = rootObject;
            }
            return View();
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