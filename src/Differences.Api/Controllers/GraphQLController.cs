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
        private readonly ISchema _schema;
        private static IDocumentExecuter _executer;
        public GraphQLController(ISchema schema,
            IDocumentExecuter executer)
        {
            _schema = schema;
            _executer = executer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] GraphQLRequest query)
        //{
        //    var result = await DataLoaderContext.Run(ctx => _executer.ExecuteAsync(_ =>
        //    {
        //        _.OperationName = query.OperationName;
        //        _.Schema = _schema;
        //        _.Query = query.Query;
        //        _.Inputs = query.Variables.ToInputs();
        //        _.UserContext = new GraphQLUserContext(ctx);
        //    }));

        //    if (result.Errors?.Count > 0)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(result);
        //}
    }
}
