using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLoader;
using Differences.Api.Model;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Differences.Api.Controllers
{
    [Route("api/[controller]")]
    public class GraphQLController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
