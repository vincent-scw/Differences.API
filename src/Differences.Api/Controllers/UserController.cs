using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;

namespace Differences.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET api/values
        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var user = _userRepository.SingleOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();

            return Json(user);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _userRepository.Add(new User() { Name = "123" });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
