using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            string s;
            var r = HttpContext.User.Claims;
            foreach (var t in r)
            { if (t.Type == "name") {
                    s = t.Value;
                } }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ApiCall()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var data =await (await client.GetAsync($"https://localhost:44321/api/orders")).Content.ReadAsStringAsync();
                List<Orders> ol = JsonConvert.DeserializeObject<List<Orders>>(data);
                return View(ol);

            }
            //return View();    
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
