using System;
using Microsoft.AspNetCore.Mvc;

namespace Differences.Api.Controllers
{
    [Route("api")]
    public class HomeController : Controller
    {
        [HttpGet]
        public string Health()
        {
            return "I am working hard...";
        }
    }
}
