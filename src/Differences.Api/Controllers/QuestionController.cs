using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Differences.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetQuestion()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Ask()
        {
            throw new NotImplementedException();
        }
    }
}
