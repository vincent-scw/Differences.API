using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Differences.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Differences.Interaction.Models;
using Differences.Interaction.Repositories;

namespace Differences.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            var user = _userRepository.Find(x => x.Id == id);
            if (user == null)
                return NotFound();

            return Json(user);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]UserModel value)
        {
            _userRepository.Add(_mapper.Map<User>(value));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]UserModel value)
        {
            _userRepository.Update(id, _mapper.Map<User>(value));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _userRepository.Remove(id);
        }
    }
}
