using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Differences.Interaction.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Differences.Api.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IArticleRepository _articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetArticles(string categoryId, [FromQuery] int offset, [FromQuery] int limit)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(string id)
        {
            return Ok();
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id)
        {
        }
    }
}
