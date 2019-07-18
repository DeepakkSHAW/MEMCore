using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MEMCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace MEMCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private IExpenseRepository _expenseRepository;
        //public ValuesController( IExpenseRepository expenseRepository)
        //{
        //    _expenseRepository = expenseRepository ?? throw new ArgumentException(nameof(expenseRepository));
        //}
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
