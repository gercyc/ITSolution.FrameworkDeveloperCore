using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITSolution.Framework.Core.CustomUserAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ITSolution.Framework.Servers.Core.FirstAPI.BaseAPIs
{
    /// <summary>
    /// Customize or create new APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MyCustomController : ControllerBase
    {
        //TODO:
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ValuesModel>> Get()
        {
            return new ValuesModel[] { new ValuesModel() };
        }

        //TODO:
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        //TODO:
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        //TODO:
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //TODO:
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
