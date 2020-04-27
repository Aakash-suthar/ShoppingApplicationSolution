using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShipmentApp.Models;

namespace ShipmentApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

       // [Authorize]
        public async Task<IActionResult> Index()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=demostorageshopping;AccountKey=y7kWOLEjUvNJQcf6LO0+PRm7ZFtcAFku41sTN5ZZbnT/zTOb/aeP1Gl++EhHmdOvpBwjt6q8yn8Ykdw7pxuPow==;EndpointSuffix=core.windows.net";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("demoqueue");
            await queue.CreateIfNotExistsAsync();
            var retrievedMessage = queue.GetMessage();
            Orders order = JsonConvert.DeserializeObject<Orders>(retrievedMessage.AsString);

            Shipmentagent3 s = new Shipmentagent3();
            s.DeliveryGuy = "Nimesh";
             s.Orderid = order.Id;
            s.Statuss = "In Transit";

            using (var client = new HttpClient())
            {
                /* var token = await HttpContext.GetTokenAsync("access_token");
                 client.DefaultRequestHeaders.Authorization =
                  new AuthenticationHeaderValue("Bearer", token);*/
                 client.BaseAddress = new Uri("https://shipmentapinew.azurewebsites.net/api/shipmentagent3");
              ////  client.BaseAddress = new Uri("https://localhost:44359/api/shipmentagent3");
                var postTask = await client.PostAsJsonAsync<Shipmentagent3>("shipmentagent3", s);
                //postTask.Wait();
              //  var result = postTask.Result;
            }
                queue.DeleteMessage(retrievedMessage);
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
