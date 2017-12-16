using System;
using Microsoft.AspNetCore.Mvc;

namespace Differences.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();    
        }
    }
}
