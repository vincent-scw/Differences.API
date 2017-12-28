using System;
using Microsoft.AspNetCore.Mvc;

namespace Differences.Api.Controllers
{
    [Route("api")]
    public class HomeController : Controller
    {
        private B2CGraphClient.GraphClient graphClient;
        public HomeController(B2CGraphClient.GraphClient client)
        {
            graphClient = client;
        }

        [HttpGet]
        public string Health()
        {
            var updateUser = graphClient.UpdateUser("7716947C-F82E-4BF5-8819-FBD75C612C48", "{\"displayName\": \"test1\"}").Result;
            return "I am working hard...";
        }
    }
}
