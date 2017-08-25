using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Differences.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    public class ConnectionController : Controller
    {
        [HttpGet]
        [Route("ping_secure")]
        public string PingSecured()
        {
            return "All good. You only get this message if you are authenticated.";
        }
    }
}
