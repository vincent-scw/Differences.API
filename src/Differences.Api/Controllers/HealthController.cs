using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Differences.Api.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("ping_secure")]
        public string PingSecured()
        {            
            return "All good. You only get this message if you are authenticated.";
        }

        [HttpGet]
        [Route("ping")]
        public string Ping()
        {
            return "All good.";
        }
    }
}
