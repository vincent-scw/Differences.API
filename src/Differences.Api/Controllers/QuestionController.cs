using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Differences.Interaction.Repositories;
using Differences.Domain.Questions;
using Differences.Api.Model;

namespace Differences.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionService _questionService;

        public QuestionController(
            IQuestionRepository questionRepository,
            IQuestionService questionService)
        {
            _questionRepository = questionRepository;
            _questionService = questionService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(string id)
        {
            var result = await _questionRepository.GetAsync(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Ask([FromBody]QuestionModel model)
        {
            await _questionService.AskQuestionAsync(model.Title, model.Tags, model.Content);
            return Ok();
        }
    }
}
