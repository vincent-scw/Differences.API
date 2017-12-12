using System;
using Microsoft.AspNetCore.Mvc;

namespace Differences.Api.Controllers
{
    [Route("")]
    public class HomeController
    {
        [HttpGet]
        public string Index()
        {
            return "I am working...";    
        }
    }
}
