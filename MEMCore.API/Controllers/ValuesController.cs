using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEMCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MEMCore.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("Info10")]
        [MapToApiVersion("1.0")]
        public IActionResult GetInfo1()
        {
            return Ok(new { message = "v1.0: all is good" });
        }
        [HttpGet("Info12")]
        [MapToApiVersion("1.2")]
        public IActionResult GetInfo2()
        {
            return Ok(new { message = "v1.1: all is good" });
        }
        private LinkGenerator _linkGenerator;
        //private IExpenseRepository _expenseRepository;
        //public ValuesController( IExpenseRepository expenseRepository)
        //{
        //    _expenseRepository = expenseRepository ?? throw new ArgumentException(nameof(expenseRepository));
        //}
        // GET api/values
        public ValuesController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get1()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get2(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            int anID = 5;
            //approach2: get location url, not working need to check
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + _linkGenerator.GetPathByAction("Get2", "Values", anID);
            var location = _linkGenerator.GetPathByAction("Get1", "Values", anID);
            var link = _linkGenerator.GetPathByAction("Get1", "Values", values: new { moniker = anID });
            return Created(location, new { DummyReturn = value });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
