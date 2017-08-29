using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Differences.Authorization.Attributes;

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
    }
}
